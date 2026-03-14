using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    
    public sealed record UpdateAddressRequest(Guid Id, string? Street = null, string? PostalCode = null, string? AppartmentNumber = null, double? Lat = null, double? Lng = null, AddressLabel? Label = null) ;

}
