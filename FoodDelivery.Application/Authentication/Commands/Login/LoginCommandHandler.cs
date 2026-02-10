using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Login;

public sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IDateTimeProvider dateTimeProvider)
        : IRequestHandler<LoginCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);

        // 🟥 401 – Invalid credentials
        var user = await userRepository.GetByEmailAsync(
            email,
            cancellationToken)??throw new UnauthorizedAccessException("Invalid email or password.");

        // 🔐 Verify password
        var passwordValid = passwordHasher.Verify(
            command.Password,
            user.PasswordHash.Value);

        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var now = dateTimeProvider.UtcNow;

        // 🧾 Audit
        user.RecordSuccessfulLogin(now);

        // 🔁 Issue refresh token (inside aggregate)
        var refreshTokenValue =
            jwtTokenGenerator.GenerateRefreshTokenValue();

        var refreshTokenExpiry =
            now.AddDays(7);

        user.IssueRefreshToken(
            refreshTokenValue,
            refreshTokenExpiry,
            now);

        // 💾 Save aggregate
        await userRepository.UpdateAsync(user, cancellationToken);

        // 🔑 Generate access token
        var accessToken =
            jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthenticationResult(
            user,
            accessToken,
            refreshTokenValue);
    }
}
