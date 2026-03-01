using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Authentication
{
 public sealed record   ResetPasswordRequest(string Email , string Password,string Otp);
}
