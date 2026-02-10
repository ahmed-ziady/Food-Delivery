using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.UserAggregate.Entities;
using FoodDelivery.Domain.UserAggregate.Enums;
using FoodDelivery.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private readonly List<RefreshToken> _refreshTokens = [];

    private User() { } // For EF Core

    private User(
        UserId id,
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        PasswordHash passwordHash,
        UserRole role,
        DateTime createdAtUtc)
        : base(id)
    {
        FirstName = NormalizeName(firstName, nameof(firstName));
        LastName = NormalizeName(lastName, nameof(lastName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Role = role ?? throw new ArgumentNullException(nameof(role));

        Status = UserStatus.Active;

        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = createdAtUtc;
        PasswordChangedAtUtc = createdAtUtc;
    }

    // -------------------------
    // Core State
    // -------------------------
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public PasswordHash PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; } = null!;

    public UserStatus Status { get; private set; }

    // -------------------------
    // Audit
    // -------------------------
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    public DateTime PasswordChangedAtUtc { get; private set; }
    public DateTime? LastLoginAtUtc { get; private set; }

    public DateTime? DeletedAtUtc { get; private set; }

    // -------------------------
    // Tokens
    // -------------------------
    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    // -------------------------
    // Derived
    // -------------------------
    public bool IsActive => Status == UserStatus.Active;
    public bool IsDisabled => Status == UserStatus.Disabled;
    public bool IsDeleted => Status == UserStatus.Deleted;

    public string FullName => $"{FirstName} {LastName}";

    // -------------------------
    // Factory
    // -------------------------
    public static User Create(
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        PasswordHash passwordHash,
        UserRole role,
        DateTime? utcNow = null)
    {
        var now = utcNow ?? DateTime.UtcNow;

        return new User(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            phoneNumber,
            passwordHash,
            role,
            now);
    }

    // -------------------------
    // Profile Operations
    // -------------------------
    public void UpdateProfile(
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        FirstName = NormalizeName(firstName, nameof(firstName));
        LastName = NormalizeName(lastName, nameof(lastName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));

        Touch(utcNow);
    }

    public void ChangeEmail(Email newEmail, DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail));
        Touch(utcNow);
    }

    public void ChangePhoneNumber(PhoneNumber newPhoneNumber, DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        PhoneNumber = newPhoneNumber ?? throw new ArgumentNullException(nameof(newPhoneNumber));
        Touch(utcNow);
    }

    // -------------------------
    // Security Operations
    // -------------------------
    public void ChangePassword(PasswordHash newPasswordHash, DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        var now = utcNow ?? DateTime.UtcNow;

        PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
        PasswordChangedAtUtc = now;

        // best practice: revoke all refresh tokens when password changes
        RevokeAllRefreshTokensInternal("Password changed", now);

        UpdatedAtUtc = now;
    }

    public void RecordSuccessfulLogin(DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        var now = utcNow ?? DateTime.UtcNow;
        LastLoginAtUtc = now;

        UpdatedAtUtc = now;
    }

    // -------------------------
    // Role / Access
    // -------------------------
    public void ChangeRole(UserRole newRole, DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        Role = newRole ?? throw new ArgumentNullException(nameof(newRole));
        Touch(utcNow);
    }

    // -------------------------
    // Lifecycle
    // -------------------------
    public void Disable(string? reason = null, DateTime? utcNow = null)
    {
        EnsureNotDeleted();

        var now = utcNow ?? DateTime.UtcNow;

        Status = UserStatus.Disabled;
        RevokeAllRefreshTokensInternal(reason ?? "User disabled", now);

        UpdatedAtUtc = now;
    }

    public void Activate(DateTime? utcNow = null)
    {
        if (IsDeleted)
            throw new InvalidOperationException("Deleted user cannot be activated.");

        Status = UserStatus.Active;
        Touch(utcNow);
    }

    public void SoftDelete(DateTime? utcNow = null)
    {
        if (IsDeleted)
            return; // idempotent

        var now = utcNow ?? DateTime.UtcNow;

        Status = UserStatus.Deleted;
        DeletedAtUtc = now;

        RevokeAllRefreshTokensInternal("User deleted", now);

        UpdatedAtUtc = now;
    }

    // -------------------------
    // Refresh Token Operations
    // -------------------------
    public RefreshToken IssueRefreshToken(string token, DateTime expiresAtUtc, DateTime? utcNow = null)
    {
        EnsureCanIssueTokens();

        var now = utcNow ?? DateTime.UtcNow;

        var refreshToken = RefreshToken.Create(token, now, expiresAtUtc);
        _refreshTokens.Add(refreshToken);

        UpdatedAtUtc = now;
        return refreshToken;
    }

    public bool RevokeRefreshToken(string token, string? reason = null, string? replacedByToken = null, DateTime? utcNow = null)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token is required.", nameof(token));

        var now = utcNow ?? DateTime.UtcNow;

        var existing = _refreshTokens.FirstOrDefault(t => t.Token == token);
        if (existing is null)
            return false;

        existing.Revoke(now, reason, replacedByToken);
        UpdatedAtUtc = now;

        return true;
    }

    public void RevokeAllRefreshTokens(string? reason = null, DateTime? utcNow = null)
    {
        var now = utcNow ?? DateTime.UtcNow;
        RevokeAllRefreshTokensInternal(reason, now);
        UpdatedAtUtc = now;
    }

    public int RemoveExpiredRefreshTokens(DateTime? utcNow = null)
    {
        var now = utcNow ?? DateTime.UtcNow;

        var before = _refreshTokens.Count;
        _refreshTokens.RemoveAll(t => t.IsExpired(now));

        var removed = before - _refreshTokens.Count;
        if (removed > 0)
            UpdatedAtUtc = now;

        return removed;
    }

    // -------------------------
    // Internal helpers
    // -------------------------
    private void RevokeAllRefreshTokensInternal(string? reason, DateTime now)
    {
        foreach (var token in _refreshTokens)
        {
            if (token.IsActive(now))
                token.Revoke(now, reason);
        }
    }

    private static string NormalizeName(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value is required.", paramName);

        var trimmed = value.Trim();

        if (trimmed.Length > 100)
            throw new ArgumentException("Value is too long.", paramName);

        return trimmed;
    }

    private void Touch(DateTime? utcNow)
    {
        UpdatedAtUtc = utcNow ?? DateTime.UtcNow;
    }

    private void EnsureNotDeleted()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Operation is not allowed for a deleted user.");
    }

    private void EnsureCanIssueTokens()
    {
        EnsureNotDeleted();

        if (!IsActive)
            throw new InvalidOperationException("Tokens cannot be issued for a disabled user.");
    }
}
