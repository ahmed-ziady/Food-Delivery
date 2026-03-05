using FoodDelivery.Application.Sections.Common;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Items.UpdateItem
{
    public sealed record UpdateItemCommand(Guid RestaurantId,
        Guid SectionId,
        Guid ItemId,
        string? Name,
        string? Description,
        decimal? Price) : IRequest<MenuItemDto>;

}
