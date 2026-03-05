namespace FoodDelivery.Application.Sections.Common
{
    public sealed record MenuItemDto(
        Guid Id,
        string Name,
        string? Description,
        decimal Price
        , IReadOnlyList<IngredientDto> Ingredients,
        IReadOnlyList<PictureDto> Pictures);
}
