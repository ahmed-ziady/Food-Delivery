using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    public sealed record SetAddressRequest(string Street, string PostalCode, string AppartmentNumber, double Lat, double Lng, AddressLabel Label);
}
