using FoodDelivery.Application.Menus.Common;
using MediatR;

namespace FoodDelivery.Application.Menus.Commands.Items.UpdateItem
{
    public sealed record UpdateItemCommand(Guid RestaurantId,
        Guid SectionId,
        Guid ItemId,
        string? Name,
        string? Description,
        decimal? Price) : IRequest<MenuSectionDto>;

}
