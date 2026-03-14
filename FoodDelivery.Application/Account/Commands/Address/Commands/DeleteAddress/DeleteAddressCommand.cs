using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.Address.Commands.DeleteAddress
{
    public sealed record DeleteAddressCommand(Guid Id) : IRequest;
    
}
