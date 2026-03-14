using FoodDelivery.Application.Account.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm
{
    public sealed record ConfirmChangeEmailCommand(Guid UserId , string NewEmail, string Otp) :IRequest<AccountResult>;
   
}
