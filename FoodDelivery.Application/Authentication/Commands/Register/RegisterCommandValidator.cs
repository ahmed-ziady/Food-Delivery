using FluentValidation;
using FoodDelivery.Application.Common.Extensions;

namespace FoodDelivery.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First name is required.")
                .NotDefaultPlaceholder()
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
                .Matches(@"^[a-zA-Z\s'-]+$")
                .WithMessage("First name contains invalid characters.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last name is required.")
                .NotDefaultPlaceholder()
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
                .Matches(@"^[a-zA-Z\s'-]+$")
                .WithMessage("Last name contains invalid characters.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .NotDefaultPlaceholder()
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Phone number is required.")
                .NotDefaultPlaceholder()
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in valid international format (E.164).");
        }
    }
}
