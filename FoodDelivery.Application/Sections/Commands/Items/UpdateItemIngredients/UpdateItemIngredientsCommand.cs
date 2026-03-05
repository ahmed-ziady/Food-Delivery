using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Commands.Items.UpdateItemIngredients
{
    public sealed record UpdateItemIngredientsCommand(
     Guid RestaurantId,
     Guid SectionId,
     Guid ItemId,
     List<Guid> IngredientIds
 ) : IRequest;
}
