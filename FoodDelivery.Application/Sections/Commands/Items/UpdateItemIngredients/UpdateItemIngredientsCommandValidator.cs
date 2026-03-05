using FluentValidation;
using FoodDelivery.Application.Sections.Commands.Items.AddIngredientsToItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Commands.Items.UpdateItemIngredients
{
    public sealed class UpdateItemIngredientsCommandValidator : AbstractValidator<AddIngredientsToItem.AddIngredientsToItemCommand>
    {
        public UpdateItemIngredientsCommandValidator()
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
