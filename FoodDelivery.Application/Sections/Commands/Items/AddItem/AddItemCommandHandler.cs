using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Domain.Entities;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Items.AddItem
{
    public sealed class AddItemCommandHandler(IMenuRepository menuRepository) : IRequestHandler<AddItemCommand, MenuSectionDto>
    {
        public async Task<MenuSectionDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)?? throw new NotFoundException("Menu.NotFound", "Menu not found.");
            var section = menu.GetSection(request.SectionId) ?? throw new NotFoundException("Section.NotFound", "Section not found.");
            var item = new MenuItem( request.Name, request.Price,request.SectionId, request.Description ,request.DeliveryType);
            section.AddItem(item);
            await menuRepository.SaveChangesAsync(cancellationToken);
            return section.Adapt<MenuSectionDto>();
        }
    }
}
