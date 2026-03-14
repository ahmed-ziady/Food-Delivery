using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Domain.Enums;
using MediatR;

namespace FoodDelivery.Application.Account.Commands.Address.Commands.UpdateAddress
{
    public sealed record UpdateAddressCommand(Guid Id, string? Street = null, string? PostalCode = null, string? AppartmentNumber = null, double? Lat = null, double? Lng = null, AddressLabel? Label = null) : IRequest<AddressResult>;

}
