using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Sections.Common;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Items.UpdateItem
{
    public sealed class UpdateItemCommnadHandler(IMenuRepository menuRepository) : IRequestHandler<UpdateItemCommand, MenuItemDto>
    {
        public async Task<MenuItemDto> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var restuarant = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
                ??throw new NotFoundException("Restuarant.NotFound", "Restuarant not found.");
            var section = restuarant.GetSection(request.SectionId)
                                ??throw new NotFoundException("Section.NotFound", "Section not found.");

            section.UpdateItem(request.ItemId, request.Name, request.Description, request.Price);
            await menuRepository.SaveChangesAsync(cancellationToken);

            return section.Adapt<MenuItemDto>();

        }
    }
}
