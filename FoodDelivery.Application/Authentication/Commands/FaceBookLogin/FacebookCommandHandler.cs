using FoodDelivery.Application.Authentication.Authentication;
using FoodDelivery.Application.Authentication.Interfaces;
using FoodDelivery.Application.Common;
using FoodDelivery.Application.Common.Interfaces.Services;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Authentication.Commands.FacebookLogin
{
    public sealed class FacebookLoginCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IDateTimeProvider dateTimeProvider,
        IFacebookAuthValidator facebookAuth)
        : IRequestHandler<FacebookLoginCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(
            FacebookLoginCommand request,
            CancellationToken cancellationToken)
        {
            var userInfo = await facebookAuth.ValidateTokenAsync(request.AccessToken);

            if (string.IsNullOrWhiteSpace(userInfo.Email))
                throw new UnauthorizedAccessException("Facebook account does not provide email.");

            var email = userInfo.Email.Trim().ToLower()??$"{userInfo.ProviderId}@facebook.local";

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new User(
                    userInfo.FirstName ?? "",
                    userInfo.LastName ?? "",
                    email,
                    ""
                )
                {
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception(errors);
                }
            }

            var now = dateTimeProvider.UtcNow;

            var refreshTokenValue = jwtTokenGenerator.GenerateRefreshTokenValue();
            var refreshTokenExpiry = now.AddDays(7);

            user.IssueRefreshToken(refreshTokenValue, refreshTokenExpiry);

            await userManager.UpdateAsync(user);

            var accessToken = await jwtTokenGenerator.GenerateAccessToken(user);

            return new AuthenticationResult(
                accessToken,
                refreshTokenValue);
        }
    }
}
