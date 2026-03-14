using FluentValidation;
using FoodDelivery.Application.Sections.Commands.Items.AddItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.AddItem
{
    public sealed class AddItemCommandValidator : AbstractValidator<AddItemCommand>
    {
        public AddItemCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty()
                .WithMessage("Restaurant Id is required.");

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .WithMessage("SectionId is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Item name is required.")
                .MaximumLength(50)
                .WithMessage("Item name must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Description must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
        }
    }
}
