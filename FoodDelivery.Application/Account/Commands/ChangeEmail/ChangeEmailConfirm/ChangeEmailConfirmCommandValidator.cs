using FluentValidation;
using FoodDelivery.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm
{
    public class ChangeEmailConfirmCommandValidator : AbstractValidator<ConfirmChangeEmailCommand>
    {
        public ChangeEmailConfirmCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.NewEmail)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Email is required.")
                    .NotDefaultPlaceholder()
                    .MaximumLength(256).WithMessage("Email must not exceed 256 characters.")
                    .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Otp)
                .NotEmpty().Length(6).WithMessage("Otp must be 6 characters long");

        }
    }
}
