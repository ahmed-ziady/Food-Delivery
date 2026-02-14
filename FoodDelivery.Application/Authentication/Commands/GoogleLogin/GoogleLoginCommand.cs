using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.GoogleLogin
{
    public sealed record GoogleLoginCommand(string IdToken) : IRequest<AuthenticationResult>;
    
}
