using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Commands.Items.UpdateItemIngredients
{
   
    public sealed class UpdateItemIngredientsCommandHandler(IMenuRepository menuRepository, IIngredientRepository ingredientRepository) : IRequestHandler<UpdateItemIngredientsCommand>
    {
        public async Task Handle(UpdateItemIngredientsCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
              ?? throw new NotFoundException("Menu.NotFound", "Menu not found.");

            var section = menu.GetSection(request.SectionId)
                ?? throw new NotFoundException("Section.NotFound", "Section not found.");

            var item = section.GetItem(request.ItemId)
                ?? throw new NotFoundException("Item.NotFound", "Item not found.");

            var ingredients = await ingredientRepository.GetByIdsAsync(request.IngredientIds, cancellationToken);

            if (ingredients.Count != request.IngredientIds.Count)
                throw new NotFoundException(
                    "Ingredient.NotFound",
                    "One or more ingredients not found");
            item.UpdateIngredients(ingredients);
            await menuRepository.SaveChangesAsync(cancellationToken);

        }
    }
}
