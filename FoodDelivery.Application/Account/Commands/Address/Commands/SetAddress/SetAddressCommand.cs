using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.Address.Commands.SetAddress
{
    public sealed record SetAddressCommand(Guid UserId, string Street, string PostalCode, string AppartmentNumber, double Lat, double Lng, AddressLabel Label) : IRequest<AddressResult>;

}
