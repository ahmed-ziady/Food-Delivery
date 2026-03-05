using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Ingredients.Common
{
     public sealed record IngredientResult(Guid Id,
        string Name,
        string ImageUrl,
        IngredientType IngredientType);
   
}
