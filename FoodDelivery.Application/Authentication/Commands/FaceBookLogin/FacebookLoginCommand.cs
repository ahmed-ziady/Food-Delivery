using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.FacebookLogin
{
    public record FacebookLoginCommand(string AccessToken) : IRequest<AuthenticationResult>;
}
