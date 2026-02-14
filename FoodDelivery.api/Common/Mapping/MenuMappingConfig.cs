using FoodDelivery.Application.Menus.Commands.CreateMenu;
using FoodDelivery.Contracts.Menus;
using FoodDelivery.Domain.Entities;
using Mapster;
using Menu = FoodDelivery.Domain.Entities.Menu;
using MenuItem = FoodDelivery.Domain.Entities.MenuItem;
using MenuSection = FoodDelivery.Domain.Entities.MenuSection;

namespace FoodDelivery.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Request → Command
        config.NewConfig<(CreateMenuRequest request, Guid userId), CreateMenuCommand>()
            .Map(dest => dest.UserId, src => src.userId)
            .Map(dest => dest, src => src.request);

        // Menu → Response
        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.AverageRating, src => src.AverageRating)
            .Map(dest => dest.Sections, src => src.Sections);

        // MenuSection → Response
        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Items, src => src.MenuItems);

        // MenuItem → Response
        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Price, src => src.Price);
    }
}
