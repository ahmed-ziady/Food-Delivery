using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Application.Account.Commands.Address.Queries
{
    public sealed class GetAddressesQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetAddressesQuery, IEnumerable<AddressResult>>
    {
        public async Task<IEnumerable<AddressResult>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await dbContext.Addresses.Where(a => a.UserId == request.UserID).ToListAsync(cancellationToken);

            return addresses.Adapt<IEnumerable<AddressResult>>();
        }
    }
}
