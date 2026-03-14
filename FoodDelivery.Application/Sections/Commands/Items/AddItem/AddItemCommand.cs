using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Domain.Enums;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Items.AddItem
{
    public sealed record AddItemCommand(Guid RestaurantId, Guid SectionId, String Name, string? Description, decimal Price, DeliveryType DeliveryType) : IRequest<MenuSectionDto>;

}
