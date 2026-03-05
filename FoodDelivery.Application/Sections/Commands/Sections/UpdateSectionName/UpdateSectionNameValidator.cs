using FluentValidation;

namespace FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName
{
    public class UpdateSectionNameValidator : AbstractValidator<UpdateSectionNameCommand>
    {
        public UpdateSectionNameValidator()
        {
            RuleFor(x => x.OwenerId).NotEmpty().WithMessage("Restaurant Id  is required.");
            RuleFor(x => x.SectionId).NotEmpty().WithMessage("Section Id is requiredd");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("new name is required.")
                .MaximumLength(100).WithMessage("Section name cannot exceed 100 characters.");
        }
    }
}