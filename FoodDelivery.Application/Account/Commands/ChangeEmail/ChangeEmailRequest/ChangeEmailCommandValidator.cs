using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailRequest
{
    public sealed class ChangeEmailCommandValidator
     : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required")
                    .NotEqual(Guid.Empty).WithMessage("UserId cannot be empty");

        }
    }



}
