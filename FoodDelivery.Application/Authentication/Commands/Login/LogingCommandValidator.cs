using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.Login
{
    public class LogingCommandValidator :AbstractValidator<LoginCommand>
    {
        public LogingCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
