using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Authentication.Commands.ForgotPassword
{
    public sealed class ForgotPasswordHandler(UserManager<User> userManager, IMailingService mailingService) : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unit.Value;
            }
            var otp = await userManager.GenerateUserTokenAsync(
               user,
               TokenOptions.DefaultEmailProvider,
               "ResetPassword");


            await mailingService.SendEmailAsync(
                request.Email,
                "Password Reset",
                $"You can reset your password using this OTP: {otp}"
            );
            return Unit.Value;
        }

    }
}
