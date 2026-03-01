using FoodDelivery.Application.Menus.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Queries.Items
{
     public sealed record GetAllItemsQuery(Guid RestaurantId, Guid SectionId) : IRequest<IReadOnlyList<MenuItemDto>>;
   
}
