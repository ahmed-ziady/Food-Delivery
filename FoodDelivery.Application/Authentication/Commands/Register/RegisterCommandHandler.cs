using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.UserAggregate;
using FoodDelivery.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Register;

public sealed class RegisterCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IDateTimeProvider dateTimeProvider)
        : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);

        // 🟥 409 – Email already exists
        var existingUser = await userRepository.GetByEmailAsync(
            email,
            cancellationToken);

        if (existingUser is not null)
            throw new InvalidOperationException("Email already exists.");

        // 🔐 Hash password
        var hashedPassword = passwordHasher.Hash(command.Password);

        // 👤 Create user (Domain factory)
        var user = User.Create(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: email,
            phoneNumber: PhoneNumber.Create(command.PhoneNumber),
            passwordHash: PasswordHash.Create(hashedPassword),
            role: UserRole.Customer,
            utcNow: dateTimeProvider.UtcNow);

        // 🔁 Issue refresh token inside aggregate
        var refreshTokenValue = jwtTokenGenerator.GenerateRefreshTokenValue();
        var refreshTokenExpiry = dateTimeProvider.UtcNow.AddDays(7);

        user.IssueRefreshToken(
            refreshTokenValue,
            refreshTokenExpiry,
            dateTimeProvider.UtcNow);

        // 💾 Save aggregate
        await userRepository.AddAsync(user, cancellationToken);

        // 🔑 Generate access token
        var accessToken = jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthenticationResult(
            user,
            accessToken,
            refreshTokenValue);
    }
}
