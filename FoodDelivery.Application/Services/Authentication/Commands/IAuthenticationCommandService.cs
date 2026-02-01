using FoodDelivery.Application.Services.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Services.Authentication.Commands
{
    public interface IAuthenticationCommandService
    {
       AuthenticationResult Register(
            string firstName,
            string lastName,
            string email,
            string password,
            string phoneNumber
            );
       AuthenticationResult Login(
            string email,
            string password);
    }
}
