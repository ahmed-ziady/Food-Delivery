using FoodDelivery.Application.Account.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm
{
    public sealed record ChangeEmailConfirmCommand(Guid UserId, string NewEmail) :IRequest<AccountResult>;
   
}
