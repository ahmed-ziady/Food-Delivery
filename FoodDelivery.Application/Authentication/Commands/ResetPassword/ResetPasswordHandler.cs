using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordHandler(UserManager<User> userManager)
            : IRequestHandler<ResetPasswordCommand, Unit>
    {
        public async Task<Unit> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                       ?? throw new UnauthorizedException(
                           "Invalid.Request",
                           "Invalid email");
            var isValid = await userManager.VerifyUserTokenAsync(
                user,
                TokenOptions.DefaultEmailProvider,
                "ResetPassword",
                request.Otp);

            if (!isValid)
                throw new UnauthorizedException(
                    "Invalid.Otp",
                    "Invalid or expired OTP.");

            // 2️⃣ Generate real reset token
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            // 3️⃣ Reset password
            var result = await userManager.ResetPasswordAsync(
                user,
                resetToken,
                request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException("Password.Reset.Failed", errors);
            }

            user.RevokeRefreshToken();
            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}