using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Menus.Common;
using FoodDelivery.Domain.Entities;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Menus.Commands.Items.AddItem
{
    public sealed class AddItemCommandHandler(IMenuRepository menuRepository) : IRequestHandler<AddItemCommand, MenuSectionDto>
    {
        public async Task<MenuSectionDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)?? throw new NotFoundException("Menu.NotFound", "Menu not found.");
            var section = menu.GetSection(request.SectionId) ?? throw new NotFoundException("Section.NotFound", "Section not found.");
            var item = new MenuItem(Guid.NewGuid(), request.Name, request.Price, request.Description);
            section.AddItem(item);
            await menuRepository.SaveChangesAsync(cancellationToken);
            return section.Adapt<MenuSectionDto>();
        }
    }
}
