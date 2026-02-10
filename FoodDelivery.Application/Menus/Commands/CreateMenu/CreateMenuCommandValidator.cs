using FluentValidation;
using FoodDelivery.Application.Common.Extensions;

namespace FoodDelivery.Application.Menus.Commands.CreateMenu
{
    public sealed class CreateMenuCommandValidator
        : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Name).NotDefaultPlaceholder()
                .NotEmpty().WithMessage("Menu name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description).NotDefaultPlaceholder()
                .NotEmpty().WithMessage("Menu description is required.")
                .MaximumLength(500);

            RuleFor(x => x.Sections)
                .NotNull().WithMessage("Menu must have sections.")
                .NotEmpty().WithMessage("Menu must have at least one section.");

            RuleForEach(x => x.Sections)
                .SetValidator(new CreateMenuSectionValidator());
        }
    }
    public sealed class CreateMenuSectionValidator
    : AbstractValidator<MenuSectionCommand>
    {
        public CreateMenuSectionValidator()
        {
            RuleFor(x => x.Name).NotDefaultPlaceholder()
                .NotEmpty().WithMessage("Section name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Section must have items.")
                .NotEmpty().WithMessage("Section must have at least one item.");

            RuleForEach(x => x.Items)
                .SetValidator(new CreateMenuItemValidator());
        }
    }
    public sealed class CreateMenuItemValidator
    : AbstractValidator<MenuItemCommand>
    {
        public CreateMenuItemValidator()
        {
            RuleFor(x => x.Name).NotDefaultPlaceholder()
                .NotEmpty().WithMessage("Item name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description).NotDefaultPlaceholder()
                
                .MaximumLength(400).WithMessage("Max length 400");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be zero or greater.");
        }
    }

}
