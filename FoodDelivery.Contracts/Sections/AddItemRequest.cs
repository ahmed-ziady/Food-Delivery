using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Contracts.Sections
{
    public sealed record AddItemRequest(string Name, string? Description, decimal Price, DeliveryType DeliveryType);

}
