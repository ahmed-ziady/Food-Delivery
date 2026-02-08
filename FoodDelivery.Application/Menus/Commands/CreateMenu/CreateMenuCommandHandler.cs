using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Host.ValueObjects;
using FoodDelivery.Domain.Menu.Entities;
using FoodDelivery.Domain.Menu.ValueObjects;
using FoodDelivery.Domain.MenuAggregate;
using FoodDelivery.Domain.MenuAggregate.Entities;
using MediatR;

namespace FoodDelivery.Application.Menus.Commands.CreateMenu
{
    public sealed class CreateMenuCommandHandler(IMenuRepository menuRepository)
        : IRequestHandler<CreateMenuCommand, Menu>
    {
        public async  Task<Menu> Handle(
                  CreateMenuCommand request,
                  CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var hostId = HostId.From(request.HostId);

            var menu = Menu.Create(
                name: request.Name,
                description: request.Description,
                hostId: hostId
            );

            foreach (var sectionRequest in request.Sections)
            {
                var section = MenuSection.Create(
                    name: sectionRequest.Name,
                    description: sectionRequest.Description
                );

                foreach (var itemRequest in sectionRequest.Items)
                {
                    var item = MenuItem.Create(
                        name: itemRequest.Name,
                        description: itemRequest.Description,
                        price: Price.Create(itemRequest.Price)
                    );

                    section.AddMenuItem(item);
                }

                menu.AddSection(section);
            }

            // 5️⃣ Persist aggregate
            menuRepository.Add(menu);
            // 6️⃣ Return response (read model)
            return menu;
        }

    }
}
