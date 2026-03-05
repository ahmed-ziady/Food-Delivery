using FoodDelivery.Application.Ingredients.Commands.AddIngredients;
using FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName;
using FoodDelivery.Application.Sections.Commands.Sections.AddSection;
using FoodDelivery.Contracts.Sections;
using Mapster;

namespace FoodDelivery.Api.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(AddMenuSectionRequest request, Guid restaurantId), AddMenuSectionCommand>()
                .Map(dest => dest.RestuarantId, src => src.restaurantId)
                .Map(dest => dest, src => src.request);



        }
    }
  

}
