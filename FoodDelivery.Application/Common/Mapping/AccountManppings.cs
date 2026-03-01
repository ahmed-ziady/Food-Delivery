using FoodDelivery.Application.Account.Common;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Mapping
{
    public static class AccountManppings
    {
        public static AccountResult ToAccountResult(this User user)
        {
            return new AccountResult(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email!,
                user.PhoneNumber,
                user.Bio,
                user.ProfilePictureUrl,
                user.EmailConfirmed,
                user.PhoneNumberConfirmed);
        }
    }
}
