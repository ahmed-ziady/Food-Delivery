using FoodDelivery.Application.Menus.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Queries.Items
{
    public sealed record  GetItemQuery(Guid RestaurantId,Guid SectionId, Guid ItemId):IRequest<MenuItemDto>;
    
}
