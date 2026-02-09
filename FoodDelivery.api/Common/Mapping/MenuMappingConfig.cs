using FoodDelivery.Application.Menus.Commands.CreateMenu;
using FoodDelivery.Contracts.Menus;
using FoodDelivery.Domain.MenuAggregate;
using Mapster;
using MenuSection = FoodDelivery.Domain.MenuAggregate.Entities.MenuSection;
using MenuItem = FoodDelivery.Domain.MenuAggregate.Entities.MenuItem;
namespace FoodDelivery.api.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Request → Command
            config.NewConfig<(CreateMenuRequest request, string hostId), CreateMenuCommand>()
                .Map(dest => dest.HostId, src => src.hostId)
                .Map(dest => dest, src => src.request);

            // Menu → Response
            config.NewConfig<Menu, MenuResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.HostId, src => src.HostId.Value)
                .Map(dest => dest.AverageRating,
                    src => src.AverageRating.NumberOfRatings > 0
                        ? src.AverageRating.Value
                        : 0)
                .Map(dest => dest.DinnerIds,
                    src => src.DinnerIds.Select(id => id.Value).ToList())
                .Map(dest => dest.MenuReviewIds,
                    src => src.MenuReviewIds.Select(id => id.Value).ToList())
                .Map(dest => dest.Sections, src => src.Sections);

            // MenuSection → Response
            config.NewConfig<MenuSection, MenuSectionResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Items, src => src.MenuItems); // 🔥 مهم

            // MenuItem → Response
            config.NewConfig<MenuItem, MenuItemResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Price, src => src.Price.Amount)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description);
                
        }
    }

}