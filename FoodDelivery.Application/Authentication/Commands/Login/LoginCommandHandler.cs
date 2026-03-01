using FoodDelivery.Application.Authentication.Authentication;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Common.Interfaces.Services;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Authentication.Commands.Login;

public sealed class LoginCommandHandler(
    UserManager<User> userManager,
    IMenuRepository menuRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<LoginCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(command.Email)??throw new UnauthorizedAccessException("Invalid email or password.");
        
        var passwordValid = await userManager.CheckPasswordAsync(
            user,
            command.Password);

        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");
        if (!user.EmailConfirmed)
            throw new UnauthorizedAccessException("Email is not confirmed.");
        var now = dateTimeProvider.UtcNow;

        var refreshTokenValue = jwtTokenGenerator.GenerateRefreshTokenValue();
        var refreshTokenExpiry = now.AddMinutes(20);

        user.IssueRefreshToken(refreshTokenValue, refreshTokenExpiry);

        await userManager.UpdateAsync(user);

        var accessToken = jwtTokenGenerator.GenerateAccessToken(user);
        if (menuRepository.GetByRestaurantIdAsync(user.Id, cancellationToken).Result is not null)
        {
            return new AuthenticationResult(
                accessToken,
                refreshTokenValue);
        }
       var menu = new Menu
        (
           Guid.NewGuid(),
            $"{user.FirstName}'s Menu",
               user.Id
       );
        
        await menuRepository.AddAsync(menu , cancellationToken);
        await menuRepository.SaveChangesAsync(cancellationToken);
        return new AuthenticationResult(
            
            accessToken,
            refreshTokenValue);
    }
}
