using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Authentication
{
   public sealed record GoogleLoginRequest (string IdToken);  
}
