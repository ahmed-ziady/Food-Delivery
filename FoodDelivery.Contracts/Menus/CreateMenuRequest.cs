namespace FoodDelivery.Contracts.Menus
{
    public record CreateMenuRequest(
        string Name,
        string Description,
        IReadOnlyList<MenuSection> Sections
    );

    public record MenuSection(
        string Name,
        IReadOnlyList<MenuItem> Items
    );

    public record MenuItem(
        string Name,
        string Description,
        decimal Price    );
}
