using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailRequest
{
    public sealed record ChangeEmailCommand(Guid UserId,string NewEmail):IRequest<Unit>;
  
}
