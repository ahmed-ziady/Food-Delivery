using FoodDelivery.Application.Menus.Common;
using FoodDelivery.Domain.Entities;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Mapping
{
    public static class MenuMappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<MenuItem, MenuItemDto>.NewConfig()
                .Map(dest => dest.Ingredients, src => src.Ingredients)
                .Map(dest => dest.Pictures, src => src.Pictures);

            TypeAdapterConfig<MenuSection, MenuSectionDto>.NewConfig()
                .Map(dest => dest.Items, src => src.Items);

            TypeAdapterConfig<MenuSection, MenuSectionDto>.NewConfig()
                .IgnoreNullValues(true);
        }
    }
}
