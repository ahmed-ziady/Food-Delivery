using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Menus.Common;
using Mapster;
using MediatR;
using System.Xml.Linq;

namespace FoodDelivery.Application.Menus.Commands.Items.UpdateItem
{
    public sealed class UpdateItemCommnadHandler(IMenuRepository menuRepository) : IRequestHandler<UpdateItemCommand, MenuSectionDto>
    {
        public async Task<MenuSectionDto> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var restuarant = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
                ??throw new NotFoundException("Restuarant.NotFound", "Restuarant not found.");
            var section = restuarant.GetSection(request.SectionId)
                                ??throw new NotFoundException("Section.NotFound", "Section not found.");

            section.UpdateItem(request.ItemId, request.Name, request.Description, request.Price);
            await menuRepository.SaveChangesAsync(cancellationToken);

            return section.Adapt<MenuSectionDto>();

        }
    }
}
