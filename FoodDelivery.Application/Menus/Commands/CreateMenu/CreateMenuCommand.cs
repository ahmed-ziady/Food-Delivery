using FoodDelivery.Domain.MenuAggregate;
using MediatR;

namespace FoodDelivery.Application.Menus.Commands.CreateMenu
{
    public record CreateMenuCommand(
        Guid HostId,
        string Name,
        string Description,
        IReadOnlyList<MenuSectionCommand> Sections
    ):IRequest<Menu >;
    public record MenuSectionCommand(
    string Name,
    string Description,
    IReadOnlyList<MenuItemCommand> Items
);

    public record MenuItemCommand(
        string Name,
        string Description,
        decimal Price
    );

}
