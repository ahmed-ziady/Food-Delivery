using FoodDelivery.Application.Authentication.Commands.ResendVerificationCode;
using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailRequest
{
    public sealed class ChangeEmailCommandHandler(
          IMailingService mailingService,
          UserManager<User> userManager)
              : IRequestHandler<ChangeEmailCommand, Unit>
    {


        public async Task<Unit> Handle(
            ChangeEmailCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null )
                return Unit.Value;

            var otp = await userManager.GenerateUserTokenAsync(
               user,
               TokenOptions.DefaultEmailProvider,
               "ChangeEmail");

            await mailingService.SendEmailAsync(
            request.NewEmail,
            " Change Email Verification",
            $"Your verification code is: {otp}");

            return Unit.Value;
        }
    }
}