using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.Logout
{
   public sealed record LogoutCommand(Guid UserId):IRequest<Unit>;
   
}
