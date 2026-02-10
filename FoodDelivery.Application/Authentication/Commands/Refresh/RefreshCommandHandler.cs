using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Refresh;

public sealed class RefreshCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IDateTimeProvider dateTimeProvider)
        : IRequestHandler<RefreshCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        RefreshCommand command,
        CancellationToken cancellationToken)
    {
        // 🔍 Find user by refresh token
        var user = await userRepository.GetByRefreshTokenAsync(
            command.RefreshToken,
            cancellationToken)??throw new UnauthorizedAccessException();
        var now = dateTimeProvider.UtcNow;

        var newRefreshTokenValue =
            jwtTokenGenerator.GenerateRefreshTokenValue();

        var refreshTokenExpiry =
            now.AddMinutes(15);

        var revoked = user.RevokeRefreshToken(
            command.RefreshToken,
            reason: "Rotated",
            replacedByToken: newRefreshTokenValue,
            utcNow: now);

        if (!revoked)
            throw new UnauthorizedAccessException();

        user.IssueRefreshToken(
            newRefreshTokenValue,
            refreshTokenExpiry,
            now);

        // 💾 Save aggregate
        await userRepository.UpdateAsync(user, cancellationToken);

        // 🔑 Generate new access token
        var accessToken =
            jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthenticationResult(
            user,
            accessToken,
            newRefreshTokenValue);
    }
}
