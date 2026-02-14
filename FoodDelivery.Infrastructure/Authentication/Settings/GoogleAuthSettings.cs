using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication.Settings
{
    public class GoogleAuthSettings
    {
        public const string SectionName = "GoogleAuthSettings";
        public string ClientId { get; set; } = default!;
        public string WebClientId { get; set; } = default!; 
    }
}
