using FoodDelivery.Application.Ingredients.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Ingredients.Queries.GetById
{
    public sealed record GetIngredientByIdQuery(Guid ID) :IRequest<IngredientResult>;
   
}
