using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Application.Account.Commands.Address.Comman
{
    public sealed record AddressResult(Guid Id, Guid UserId, string Street, string PostalCode, string AppartmentNumber, double Lat, double Lng, AddressLabel Label, bool IsDefault);
}
