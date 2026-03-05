using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Commands.Items.AddIngredientsToItem
{
    public sealed record AddIngredientsToItemCommand(Guid RestaurantId, Guid SectionId, Guid ItemId, List<Guid> IngredientIds):IRequest;
   
}
