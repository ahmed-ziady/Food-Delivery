using FoodDelivery.Application.Menus.Commands.Sections.AddSection;
using FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName;
using FoodDelivery.Contracts.Menus;
using Mapster;

namespace FoodDelivery.Api.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(AddMenuSectionRequest request, Guid restaurantId), AddMenuSectionCommand>()
                .Map(dest => dest.OwenerId, src => src.restaurantId)
                .Map(dest => dest, src => src.request);
        }
    }
  

}
