using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    public sealed record AddressResponse(Guid ID, Guid UserId, string Street, string PostalCode, string AppartmentNumber, double Lat, double Lng, AddressLabel Label, bool IsDefault);
    
}
