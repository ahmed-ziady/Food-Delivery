using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Application.Authentication.Commands.Refresh;

public sealed class RefreshCommandHandler(
    UserManager<User> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<RefreshCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        RefreshCommand command,
        CancellationToken cancellationToken)
    {
        // 🔍 Find user by refresh token
        var user = await userManager.Users
            .FirstOrDefaultAsync(
                u => u.RefreshToken == command.RefreshToken,
                cancellationToken)??throw new UnauthorizedAccessException("Invalid refresh token.");
        var now = dateTimeProvider.UtcNow;

        // ⏳ Check expiration
        if (user.RefreshTokenExpiry is null ||
            user.RefreshTokenExpiry <= now)
        {
            throw new UnauthorizedAccessException("Refresh token expired.");
        }

        // 🔁 Rotate refresh token
        var newRefreshToken = jwtTokenGenerator.GenerateRefreshTokenValue();
        var newExpiry = now.AddDays(7);

        user.IssueRefreshToken(newRefreshToken, newExpiry);

        await userManager.UpdateAsync(user);

        // 🔑 Generate new access token
        var accessToken = jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthenticationResult(
            accessToken,
            newRefreshToken);
    }
}
