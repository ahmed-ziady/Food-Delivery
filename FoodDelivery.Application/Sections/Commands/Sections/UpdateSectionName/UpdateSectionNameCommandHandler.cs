using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Domain.Commons;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName
{
    public sealed class UpdateSectionNameCommandHandler(IMenuRepository menuRepository) : IRequestHandler<UpdateSectionNameCommand, MenuSectionDto>
    {
        public async Task<MenuSectionDto> Handle(UpdateSectionNameCommand request, CancellationToken cancellationToken)
        {


            var menu = await menuRepository.GetByRestaurantIdAsync(request.OwenerId, cancellationToken) ?? throw new DomainException("Menu not found");

            menu.UpdateSectionName(request.SectionId, request.Name);

            await menuRepository.SaveChangesAsync(cancellationToken);


            var sectionDto = menu.Adapt<MenuSectionDto>();
            return sectionDto;
        }
    }
}
