using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FoodDelivery.Infrastructure.Authentication.Services;

public sealed class TwilioSmsService : ISmsService
{
    private readonly TwilioSettings _settings;

    public TwilioSmsService(IOptions<TwilioSettings> options)
    {
        _settings = options.Value;
        TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
    }

    public async Task<MessageResource> SendAsync(string phoneNumber, string message)
    {
        return await MessageResource.CreateAsync(
            body: message,
            from: new PhoneNumber(_settings.FromPhoneNumber),
            to: new PhoneNumber(phoneNumber)
        );
    }
}
