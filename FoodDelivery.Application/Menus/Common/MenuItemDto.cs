namespace FoodDelivery.Application.Menus.Common
{
    public sealed record MenuItemDto(
        Guid Id,
        string Name,
        string? Description,
        decimal Price
        , IEnumerable<IngredientDto> Ingredients,
        IEnumerable<PictureDto> Pictures);
}
