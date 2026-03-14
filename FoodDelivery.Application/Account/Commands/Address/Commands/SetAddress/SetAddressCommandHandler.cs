using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace FoodDelivery.Application.Account.Commands.Address.Commands.SetAddress
{
    public sealed class SetAddressCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<SetAddressCommand, AddressResult>
    {
        public async Task<AddressResult> Handle(SetAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                 ?? throw new UnauthorizedException("User.NotFound", "User not found.");

            var address = new Domain.Entities.Address(request.UserId, request.Street, request.PostalCode, request.AppartmentNumber, request.Lat, request.Lng ,request.Label);
            user.AddAddress(address);
            await dbContext.SaveChangesAsync(cancellationToken);
            return address.Adapt<AddressResult>();

        }
    }
}
