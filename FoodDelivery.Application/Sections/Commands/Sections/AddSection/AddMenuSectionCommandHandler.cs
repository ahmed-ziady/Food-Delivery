using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Entities;
using Mapster;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Sections.AddSection
{
    public sealed class AddMenuSectionCommandHandler(IMenuRepository menuRepository) : IRequestHandler<AddMenuSectionCommand, MenuSectionDto>
    {
        public async Task<MenuSectionDto> Handle(AddMenuSectionCommand request, CancellationToken cancellationToken)
        {


            var menu = await menuRepository.GetByRestaurantIdAsync(request.RestuarantId, cancellationToken) ?? throw new DomainException("Menu not found");
            var newSection = new MenuSection( request.Name);
            menu.AddSection(newSection);

            await menuRepository.SaveChangesAsync(cancellationToken);


            var sectionDto = newSection.Adapt<MenuSectionDto>();
            return sectionDto;
        }
    }
}
