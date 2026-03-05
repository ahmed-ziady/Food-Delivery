using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using MediatR;

namespace FoodDelivery.Application.Ingredients.Commands.DeleteIngredient
{
    public sealed class DeleteIngredientCommandHandler(IIngredientRepository ingredientRepository, IImageStorageService imageStorageService) : IRequestHandler<DeleteIngredientCommand>
    {
        public async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await ingredientRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Ingrerient.NotFound", "Ingredient is not founded");
            await imageStorageService.DeleteAsync(ingredient.ImageUrl, "ItemIngredientPictures", cancellationToken);
            ingredientRepository.Remove(ingredient);
            await ingredientRepository.SaveChanagesAsync(cancellationToken);
        }
    }
}
