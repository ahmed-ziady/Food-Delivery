using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.FacebookLogin
{
    public class FacebookLoginValidator :AbstractValidator<FacebookLoginCommand>
    {
        public FacebookLoginValidator()
            {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token is required.");
            }
    
           
    }
}
