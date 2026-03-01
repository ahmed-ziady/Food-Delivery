namespace FoodDelivery.Contracts.Menus
{
    public sealed record AddItemRequest(string Name, string? Description, decimal Price);

}
