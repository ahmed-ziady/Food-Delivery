using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Items.AddIngredientsToItem
{
    public sealed class AddIngredientsToItemCommandHandler(IMenuRepository menuRepository, IIngredientRepository ingredientRepository) : IRequestHandler<AddIngredientsToItemCommand>
    {
        public async Task Handle(AddIngredientsToItemCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
              ?? throw new NotFoundException("Menu.NotFound", "Menu not found.");

            var section = menu.GetSection(request.SectionId)
                ?? throw new NotFoundException("Section.NotFound", "Section not found.");

            var item = section.GetItem(request.ItemId)
                ?? throw new NotFoundException("Item.NotFound", "Item not found.");

            var ingredients = await ingredientRepository.GetByIdsAsync(request.IngredientIds,cancellationToken);

            if (ingredients.Count != request.IngredientIds.Count)
                throw new NotFoundException(
                    "Ingredient.NotFound",
                    "One or more ingredients not found");
            item.AddIngredients(ingredients);
            await menuRepository.SaveChangesAsync(cancellationToken);

        }
    }
}
