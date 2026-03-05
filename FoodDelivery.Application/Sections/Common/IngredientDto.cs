using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Application.Sections.Common
{
    public sealed record IngredientDto (Guid Id, string Name , string ImageUrl, IngredientType Type);
}
