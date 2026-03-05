namespace FoodDelivery.Application.Sections.Common
{
    public sealed record MenuSectionDto(
        Guid Id,
        string Name,
        IEnumerable<MenuItemDto> Items);
}

