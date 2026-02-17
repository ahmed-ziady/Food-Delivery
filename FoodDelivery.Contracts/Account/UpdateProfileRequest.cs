using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    public sealed record UpdateProfileRequest(string? FirstName, string? LastName,string? Bio);
}
