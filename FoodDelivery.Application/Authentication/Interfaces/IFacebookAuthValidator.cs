using FoodDelivery.Application.Common;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Authentication
{
    public interface IFacebookAuthValidator
    {
        Task<FacebookUserInfo> ValidateTokenAsync(string accessToken);
    }
}
