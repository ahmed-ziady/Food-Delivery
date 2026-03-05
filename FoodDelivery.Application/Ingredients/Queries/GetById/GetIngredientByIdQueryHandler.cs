using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Ingredients.Common;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Ingredients.Queries.GetById
{
    public sealed class GetIngredientByIdQueryHandler(IIngredientRepository ingredientRepository) : IRequestHandler<GetIngredientByIdQuery, IngredientResult>
    {
        public async Task<IngredientResult> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await ingredientRepository.GetByIdAsync(request.ID, cancellationToken)
                ?? throw new NotFoundException("Ingredient.NotFound", "Ingredient Not Found");
            return ingredient.Adapt<IngredientResult>();
        }
    }
}
