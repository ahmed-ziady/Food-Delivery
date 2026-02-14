using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication.Settings
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";

        public string SmtpServer { get; set; } = default!;
        public int SmtpPort { get; set; }=default!;
        public string Email { get; set; }=default!;
        public string Password { get; set; }=default!; 
        public string DisplayName { get; set; }=default!;

         
    }
}
