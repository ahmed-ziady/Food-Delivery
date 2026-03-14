using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Application.Account.Commands.Address.Commands.DeleteAddress
{
    public sealed class DeleteAddressCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteAddressCommand>
    {
        public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses
             .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)??throw new NotFoundException("Address.NotFound", "Address not found.");

            dbContext.Addresses.Remove(address);
        await    dbContext.SaveChangesAsync (cancellationToken);
        }
    }
}
