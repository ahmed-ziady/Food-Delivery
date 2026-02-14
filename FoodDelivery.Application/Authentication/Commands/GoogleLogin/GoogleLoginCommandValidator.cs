using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.GoogleLogin
{
    public class GoogleLoginCommandValidator :AbstractValidator<GoogleLoginCommand>
    {
        public GoogleLoginCommandValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty().WithMessage("IdToken is required.");
        }
    }
}
