using FoodDelivery.Application.Authentication.Authentication;
using FoodDelivery.Application.Authentication.Interfaces;
using FoodDelivery.Application.Common;
using FoodDelivery.Application.Common.Interfaces.Services;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Authentication.Commands.GoogleLogin
{
    public sealed class GoogleLoginCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IDateTimeProvider dateTimeProvider,
        IGoogleAuthValidator googleAuth)
        : IRequestHandler<GoogleLoginCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(
            GoogleLoginCommand request,
            CancellationToken cancellationToken)
        {
            var payload = await googleAuth.ValidateTokenAsync(request.IdToken);

            var email = payload.Email.Trim().ToLower();

            if (!payload.EmailVerified)
                throw new UnauthorizedAccessException("Google email is not verified.");

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new User(
                    payload.GivenName ?? "",
                    payload.FamilyName ?? "",
                    email,
                    "" 
                )
                {
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);

                if (!result.Succeeded)
                    throw new Exception("Failed to create Google user.");
            }

            var now = dateTimeProvider.UtcNow;

            var refreshTokenValue = jwtTokenGenerator.GenerateRefreshTokenValue();
            var refreshTokenExpiry = now.AddDays(7); // match your system

            user.IssueRefreshToken(refreshTokenValue, refreshTokenExpiry);

            await userManager.UpdateAsync(user);
            var accessToken =await jwtTokenGenerator.GenerateAccessToken(user);

            return new AuthenticationResult(
                accessToken,
                refreshTokenValue);
        }
    }
}
