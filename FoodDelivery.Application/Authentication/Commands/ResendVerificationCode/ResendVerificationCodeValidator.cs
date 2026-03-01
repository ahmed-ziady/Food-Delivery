using FluentValidation;

namespace FoodDelivery.Application.Authentication.Commands.ResendVerificationCode
{
    public sealed class ResendVerificationCodeValidator
    : AbstractValidator<ResendVerificationCodeCommand>
    {
        public ResendVerificationCodeValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }

}
