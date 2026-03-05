using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using MediatR;

namespace FoodDelivery.Application.Menus.Commands.Items.DeleteItem
{
    public sealed class DeleteItemCommandHandler(IMenuRepository menuRepository) : IRequestHandler<DeleteItemCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var restuarant = await menuRepository.GetByRestaurantIdAsync(request.RestuarantId, cancellationToken)
               ??throw new NotFoundException("Restuarant.NotFound", "Restuarant not found.");
            var section = restuarant.GetSection(request.SectionId)
               ??throw new NotFoundException("Section.NotFound", "Section not found.");
            section.RemoveItem(request.ItemId);
            await menuRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
