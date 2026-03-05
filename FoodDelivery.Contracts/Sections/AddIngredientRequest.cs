using FoodDelivery.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Contracts.Sections
{
    public sealed record AddIngredientRequest(
     string Name,
     IFormFile? Picture,
     IngredientType IngredientType
 );

}