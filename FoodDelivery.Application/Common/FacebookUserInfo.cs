using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common
{
    public class FacebookUserInfo
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public string? Bio { get; set; }
        public string ProviderId { get; set; } = null!;
    }
}
