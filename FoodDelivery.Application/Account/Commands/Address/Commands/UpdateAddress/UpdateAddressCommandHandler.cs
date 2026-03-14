using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Application.Account.Commands.Address.Commands.UpdateAddress
{
    public sealed class UpdateAddressCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateAddressCommand, AddressResult>
    {
        public async Task<AddressResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id==request.Id, cancellationToken)
                ?? throw new NotFoundException( "Address.NotFound", "Address not found.");

            address.Update(request.Street, request.PostalCode, request.AppartmentNumber, request.Lat, request.Lng, request.Label);
            await dbContext.SaveChangesAsync(cancellationToken);
            return address.Adapt<AddressResult>();
        }
    }
}
