using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    public record AccountResponse(
       Guid Id,
       string FirstName,
       string LastName,
       string Email,
       string? PhoneNumber,
       string? Bio,
       string? ProfilePictureUrl,
       bool EmailConfirmed,
       bool PhoneNumberConfirmed
   );

}
