using FoodDelivery.Application.Account.Commands.Address.Comman;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.Address.Queries
{
    public sealed record GetAddressesQuery(Guid UserID) : IRequest<IEnumerable<AddressResult>>;
    
}
