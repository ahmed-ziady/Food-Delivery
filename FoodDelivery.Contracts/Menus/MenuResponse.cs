
namespace FoodDelivery.Contracts.Menus
{
    public record MenuResponse(
        string Id,
        string Name,
        string Description,
        IReadOnlyList<MenuSectionResponse> Sections,
        string UserId,
        double AverageRating,
        IReadOnlyList<string> DinnerIds,
        IReadOnlyList<string> MenuReviewIds,
        DateTime CreateDateTime,
        DateTime UpdateDateTime);

    public record MenuSectionResponse(string Id,
        string Name,
        string Description,
        IReadOnlyList<MenuItemResponse> Items);
    public record MenuItemResponse(
        string Id,
        string Name,
        string Description,
        decimal Price);
}
