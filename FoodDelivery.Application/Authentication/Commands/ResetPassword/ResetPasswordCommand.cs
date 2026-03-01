using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand (string Email , string Password,string Otp):IRequest<Unit>;
    
}
