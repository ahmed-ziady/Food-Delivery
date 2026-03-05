using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Ingredients.Common;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Ingredients.Queries.GetAll
{
    public sealed class GetAllIngredientsHandler(IIngredientRepository ingredientRepository) : IRequestHandler<GetAllIngredientsQuery, IReadOnlyList<IngredientResult>>
    {
        public async Task<IReadOnlyList<IngredientResult>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients= await ingredientRepository.GetAllAsync(cancellationToken) ;

            return [.. ingredients.Select(i => new IngredientResult (i.Id, i.Name, i.ImageUrl ,i.Type)).OrderBy(i => i.Name)];
        }
    }
}
