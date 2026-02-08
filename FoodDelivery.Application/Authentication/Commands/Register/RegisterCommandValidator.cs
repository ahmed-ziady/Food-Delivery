using FluentValidation;
using FoodDelivery.Application.Common.Extensions;

namespace FoodDelivery.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.").NotDefaultPlaceholder()
                .MaximumLength(20).WithMessage("First name must not exceed 20 characters.");
            RuleFor(x => x.LastName).NotEmpty().NotDefaultPlaceholder()
                .WithMessage("Last name is required.")
                .MaximumLength(20).WithMessage("Last name must not exceed 20 characters.");
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required.").NotDefaultPlaceholder()
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotDefaultPlaceholder()
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid phone number is required.");
        }
    }
}
