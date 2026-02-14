namespace FoodDelivery.Infrastructure.Authentication.Settings
{
    public sealed class TwilioSettings
    {
        public const string SectionName = "TwilioSettings";
        public string AccountSid { get; set; } = default!;
        public string AuthToken { get; set; } = default!;
        public string FromPhoneNumber { get; set; } = default!;

    }
}
