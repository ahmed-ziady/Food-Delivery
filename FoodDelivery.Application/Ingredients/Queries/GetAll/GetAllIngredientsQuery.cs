using FoodDelivery.Application.Ingredients.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Ingredients.Queries.GetAll
{
     public sealed record GetAllIngredientsQuery ():IRequest<IReadOnlyList<IngredientResult>>;
   
}
