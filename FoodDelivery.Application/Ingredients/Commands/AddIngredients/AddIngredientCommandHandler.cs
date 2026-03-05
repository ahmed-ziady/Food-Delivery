using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Ingredients.Commands.AddIngredients
{

    public sealed class AddIngredientCommandHandler(
    IIngredientRepository ingredientRepository,
    IImageStorageService imageStorageService)
    : IRequestHandler<AddIngredientCommand>
    {
        public async Task Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {

            var url = await imageStorageService
             .UploadAsync(request.Picture!, "ItemIngredientPictures", cancellationToken);
            var ingredient = new Ingredient(request.Name, url, request.IngredientType);
            await ingredientRepository.AddAsync(ingredient, cancellationToken);
            await ingredientRepository.SaveChanagesAsync(cancellationToken);
        }
    }
}
