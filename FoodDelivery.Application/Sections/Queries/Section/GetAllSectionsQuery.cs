using FoodDelivery.Application.Sections.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Queries.Section
{
    public sealed record GetAllSectionsQuery(Guid RestaurantId):IRequest<IReadOnlyList<SectionResult>>;
   
}
