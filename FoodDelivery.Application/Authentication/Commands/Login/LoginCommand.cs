using FoodDelivery.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.Login
{
   public record LoginCommand(string Email,
        string Password) : IRequest<AuthenticationResult>;

}
