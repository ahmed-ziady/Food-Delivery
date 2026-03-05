using FoodDelivery.Application.Ingredients.Commands.AddIngredients;
using FoodDelivery.Contracts.Sections;
using Mapster;

namespace FoodDelivery.Api.Common.Mapping
{
    public class IngredientsMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddIngredientRequest, AddIngredientCommand>()
                  .Map(dest => dest.Name, src => src.Name)
                  .Map(dest => dest.IngredientType, src => src.IngredientType)
                  .Map(dest => dest.Picture, src => src.Picture); 
        }
    }
}