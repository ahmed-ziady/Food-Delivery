using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Sections.RemoveSection
{
    public sealed class RemoveSectionCommandValidator : AbstractValidator<RemoveSectionCommand>
    {
        public RemoveSectionCommandValidator()
        {
            RuleFor(x => x.restaurantId).NotEmpty().WithMessage("Restaurant Id is required");
            RuleFor(x => x.SectionId).NotEmpty().WithMessage("Section Id is required");

        }
    }
}
