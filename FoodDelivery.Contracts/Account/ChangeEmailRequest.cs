using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Account
{
    public sealed record ChangeEmailRequest(string NewEmail, string Otp);
}
