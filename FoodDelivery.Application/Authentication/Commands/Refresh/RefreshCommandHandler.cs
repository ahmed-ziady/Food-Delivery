using FoodDelivery.Application.Authentication.Interfaces;
using FoodDelivery.Application.Common;
using FoodDelivery.Application.Common.Interfaces.Services;
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
        var user = await userManager.Users
            .FirstOrDefaultAsync(
                u => u.RefreshToken == command.RefreshToken,
                cancellationToken)??throw new UnauthorizedAccessException("Invalid refresh token.");
        var now = dateTimeProvider.UtcNow;

        if (user.RefreshTokenExpiry <= now)
        {
            throw new UnauthorizedAccessException("Refresh token expired.");
        }

        var newRefreshToken = jwtTokenGenerator.GenerateRefreshTokenValue();
        var newExpiry = now.AddMinutes(20);

        user.IssueRefreshToken(newRefreshToken, newExpiry);

        await userManager.UpdateAsync(user);

        var accessToken =await jwtTokenGenerator.GenerateAccessToken(user);

        return new AuthenticationResult(
            accessToken,
            newRefreshToken);
    }
}
