using FluentValidation;
using FoodDelivery.Application.Common.Extensions;

namespace FoodDelivery.Application.Account.Commands.UpdateProfile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotDefaultPlaceholder()
                .MaximumLength(50)
                    .WithMessage("First name must not exceed 50 characters.")
                .Matches(@"^[a-zA-Z\s'-]+$")
                    .WithMessage("First name contains invalid characters.")
                .When(x => x.FirstName is not null);

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotDefaultPlaceholder()
                .MaximumLength(50)
                    .WithMessage("Last name must not exceed 50 characters.")
                .Matches(@"^[a-zA-Z\s'-]+$")
                    .WithMessage("Last name contains invalid characters.")
                .When(x => x.LastName is not null);

            RuleFor(x => x.Bio)
                .Cascade(CascadeMode.Stop).NotDefaultPlaceholder()
                .MaximumLength(300)
                    .WithMessage("Bio must not exceed 300 characters.")
                .When(x => x.Bio is not null);
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .NotEqual(Guid.Empty).WithMessage("User ID must be a valid GUID.");

        }
    }
}