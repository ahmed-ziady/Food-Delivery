using FluentValidation;
using FoodDelivery.Application.Common.Extensions;

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
            RuleFor(x => x.NewEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .NotDefaultPlaceholder()
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.")
                .EmailAddress().WithMessage("Invalid email format.");


        }
    }



}
