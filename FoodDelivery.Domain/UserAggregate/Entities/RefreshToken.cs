using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.UserAggregate.ValueObjects;

namespace FoodDelivery.Domain.UserAggregate.Entities;

public sealed class RefreshToken : Entity<RefreshTokenId>
{
    public string Token { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime? RevokedAtUtc { get; private set; }
    public string? RevokedReason { get; private set; }
    public string? ReplacedByToken { get; private set; }

    private RefreshToken() { } // For EF Core

    private RefreshToken(
        RefreshTokenId id,
        string token,
        DateTime createdAtUtc,
        DateTime expiresAtUtc)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Refresh token is required.", nameof(token));

        if (createdAtUtc == default)
            throw new ArgumentException("CreatedAtUtc is required.", nameof(createdAtUtc));

        if (expiresAtUtc <= createdAtUtc)
            throw new ArgumentException(
                "Refresh token expiry must be after creation time.",
                nameof(expiresAtUtc));

        Token = token;
        CreatedAtUtc = createdAtUtc;
        ExpiresAtUtc = expiresAtUtc;
    }

    // -------------------------
    // Factory
    // -------------------------
    public static RefreshToken Create(
        string token,
        DateTime createdAtUtc,
        DateTime expiresAtUtc)
    {
        return new RefreshToken(
            RefreshTokenId.CreateUnique(),
            token,
            createdAtUtc,
            expiresAtUtc);
    }

    // -------------------------
    // State
    // -------------------------
    public bool IsExpired(DateTime utcNow)
        => utcNow >= ExpiresAtUtc;

    public bool IsRevoked
        => RevokedAtUtc is not null;

    public bool IsActive(DateTime utcNow)
        => !IsRevoked && !IsExpired(utcNow);

    // -------------------------
    // Behavior
    // -------------------------
    public void Revoke(
        DateTime revokedAtUtc,
        string? reason = null,
        string? replacedByToken = null)
    {
        if (IsRevoked)
            return; // idempotent

        if (revokedAtUtc < CreatedAtUtc)
            throw new InvalidOperationException(
                "Revoke date cannot be before creation date.");

        RevokedAtUtc = revokedAtUtc;

        RevokedReason = string.IsNullOrWhiteSpace(reason)
            ? null
            : reason.Trim();

        ReplacedByToken = string.IsNullOrWhiteSpace(replacedByToken)
            ? null
            : replacedByToken.Trim();
    }
}
