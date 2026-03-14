using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    public sealed record ConfirmChangeEmailRequest(string NewEmail, string Otp);
}
