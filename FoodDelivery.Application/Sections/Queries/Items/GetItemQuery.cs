using FoodDelivery.Application.Sections.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Queries.Items
{
    public sealed record  GetItemQuery(Guid RestaurantId,Guid SectionId, Guid ItemId):IRequest<MenuItemDto>;
    
}
