using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Sections.AddSection
{
    public class AddMenuSectionValidator : AbstractValidator<AddMenuSectionCommand>
    {
        public AddMenuSectionValidator()
        {
            RuleFor(x => x.OwenerId).NotEmpty().WithMessage("MenuId is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Section name is required.")
                .MaximumLength(100).WithMessage("Section name cannot exceed 100 characters.");
        }
    }
}
