using FoodDelivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Common
{
    public sealed record MenuSectionDto(
        Guid Id,
        string Name,
        IEnumerable<MenuItemDto> Items);
}

