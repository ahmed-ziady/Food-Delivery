using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication.Settings
{
    public class FacebookAuthSettings
    {
        public const string SectionName = "FacebookAuthSettings";
        public string AppId { get; set; } = default!;
        public string AppSecret { get; set; } = default!;
    }
}
