using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Sections.Common;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Sections.Queries.Items
{
    public sealed class GetItemQueryHandler(IMenuRepository menuRepository) : IRequestHandler<GetItemQuery, MenuItemDto>
    {
        async Task<MenuItemDto> IRequestHandler<GetItemQuery, MenuItemDto>.Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
            ?? throw new NotFoundException("Item.NotFound", "Item Not Founded");
            var section = restaurant.GetSection(request.SectionId)
                ?? throw new NotFoundException("Section.NotFound", "Section Not Founded");
            var item = section.GetItem(request.ItemId);
            return  item.Adapt<MenuItemDto>();

        }
    }
}
