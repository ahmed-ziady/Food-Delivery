using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.ResendVerificationCode
{
    public sealed class ResendVerificationCodeHandler(
        IMailingService mailingService,
        UserManager<User> userManager)
            : IRequestHandler<ResendVerificationCodeCommand, Unit>
    {


        public async Task<Unit> Handle(
            ResendVerificationCodeCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null || user.EmailConfirmed)
                return Unit.Value;

            var otp =     await userManager.GenerateUserTokenAsync(
               user,
               TokenOptions.DefaultEmailProvider,
               "EmailConfirmation");

            await mailingService.SendEmailAsync(
            user.Email!,
            "Email Verification",
            $"Your verification code is: {otp}");

            return Unit.Value;
        }
    }
}