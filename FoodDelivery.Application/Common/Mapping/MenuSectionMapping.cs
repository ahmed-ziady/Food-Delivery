using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.ValueObjects;
using Mapster;

namespace FoodDelivery.Application.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MenuItem, MenuItemDto>()
                .Map(dest => dest.Ingredients,
                     src => src.MenuItemIngredients
                               .Select(mii => new IngredientDto(
                                   mii.Ingredient.Id,
                                   mii.Ingredient.Name,
                                   mii.Ingredient.ImageUrl,
                                   mii.Ingredient.Type)))
                .Map(dest => dest.Pictures,
                     src => src.Pictures
                               .Select(p => new PictureDto(p.Url)));

            config.NewConfig<MenuSection, MenuSectionDto>()
                .Map(dest => dest.Items,
                     src => src.Items.Select(i => i.Adapt<MenuItemDto>()))
                .IgnoreNullValues(true);

            config.NewConfig<Ingredient, IngredientDto>();

            // Picture -> PictureDto
            config.NewConfig<Picture, PictureDto>();


            TypeAdapterConfig<Address, AddressResult>.NewConfig()
            .Map(dest => dest.Lat, src => src.Location.Y)
            .Map(dest => dest.Lng, src => src.Location.X);
        }
    }
}
