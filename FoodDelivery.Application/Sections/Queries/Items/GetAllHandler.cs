using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Application.Sections.Queries.Items;
using MapsterMapper;
using MediatR;

namespace FoodDelivery.Application.Menus.Queries.Items
{
    public sealed class GetAllItemsQueryHandler(IMenuRepository menuRepository, IMapper mapper) : IRequestHandler<GetAllItemsQuery, IReadOnlyList<MenuItemDto>>
    {
        public async Task<IReadOnlyList<MenuItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
                       ?? throw new NotFoundException("Menu.NotFound", "Menu not found.");

            var section = restaurant.GetSection(request.SectionId)
                          ?? throw new NotFoundException("Section.NotFound", "Section not found.");

            var itemsDto = section.Items
                .Select(item => mapper.Map<MenuItemDto>(item))
                .ToList();

            return itemsDto;
        }
    }
}