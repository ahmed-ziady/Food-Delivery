using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.ResendVerificationCode
{
   public sealed record ResendVerificationCodeCommand (string Email):IRequest<Unit>;
   
}
