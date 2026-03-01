using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Application.Menus.Common
{
    public sealed record IngredientDto (Guid Id, string Name , string ImageUrl, IngredientType Type);
}
