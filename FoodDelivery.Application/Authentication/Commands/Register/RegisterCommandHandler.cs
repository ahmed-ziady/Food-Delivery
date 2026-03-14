using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Application.Authentication.Commands.Register
{
    public sealed class RegisterCommandHandler(
        IUserService userService,
        IMailingService mailingService)
                : IRequestHandler<RegisterCommand, Unit>
    {
        public async Task<Unit> Handle(
            RegisterCommand command,
            CancellationToken cancellationToken)
        {
            var email = command.Email.Trim().ToLower();

            var existingUser = await userService.GetByEmailAsync(email);

            if (existingUser is not null)
                throw new InvalidOperationException("Email already exists.");

            var user = new User(
                command.FirstName.Trim(),
                command.LastName.Trim(),
                email,
                command.PhoneNumber.Trim()
            );

            var result = await userService.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException(errors);
            }

            // Generate OTP
            var token = await userService.GenerateEmailTokenAsync(
                user);

            // Send Email
            await mailingService.SendEmailAsync(
                user.Email!,
                "Verification Code",
                $"Your verification code is: {token}");


            return Unit.Value;
        }
    }
}
