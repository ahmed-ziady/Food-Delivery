using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Commands.Items.AddIngredientsToItem
{
    public sealed class AddIngredientsToItemCommandValidator:AbstractValidator<AddIngredientsToItemCommand>
    {
        public AddIngredientsToItemCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
               .NotEmpty()
               .WithMessage("Restaurant Id is required.");

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .WithMessage("SectionId is required.");

            RuleFor(x => x.ItemId)
                .NotEmpty()
                .WithMessage("ItemId is required.");

            RuleFor(x => x.IngredientIds)
                .NotNull()
                .WithMessage("At least should add one ingredient");
        }
    }
}
