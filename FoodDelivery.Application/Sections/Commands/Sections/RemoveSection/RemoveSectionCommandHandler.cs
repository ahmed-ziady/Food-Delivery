using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Sections.RemoveSection
{
    public sealed class RemoveSectionCommandHandler(IMenuRepository menuRepository) : IRequestHandler<RemoveSectionCommand, Unit>
    {
       public async Task<Unit> Handle(RemoveSectionCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetByRestaurantIdAsync(request.restaurantId, cancellationToken) ?? throw new DomainException(nameof(request));

           menu.RemoveSection(request.SectionId);
            await menuRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
