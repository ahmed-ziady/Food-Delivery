using FoodDelivery.Application.Authentication.Authentication;
using FoodDelivery.Application.Common.Interfaces.Services;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Infrastructure.Authentication.Services
{
    public class VerificationOtpService(UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator,
    IDateTimeProvider dateTimeProvider) : IVerifyOtp
    {
        public async Task<AuthenticationResult> VerifyOtpAsync(string email, string otp)
        {
            var user = await userManager.FindByEmailAsync(email)??throw new ApplicationException("Invalid OTP or email.");

            var isValid = await userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, otp);
            if (!isValid)
            {
                throw new Exception("Invalid OTP.");
            }
            user.EmailConfirmed = true;
            var now = dateTimeProvider.UtcNow;

            var refreshTokenValue = jwtTokenGenerator.GenerateRefreshTokenValue();
            var refreshTokenExpiry = now.AddMinutes(20);

            user.IssueRefreshToken(refreshTokenValue, refreshTokenExpiry);

            await userManager.UpdateAsync(user);

            var accessToken = jwtTokenGenerator.GenerateAccessToken(user);

            return new AuthenticationResult(
                accessToken,
                refreshTokenValue);

        }
    }
}
