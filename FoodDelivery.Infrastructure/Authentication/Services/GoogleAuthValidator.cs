using FoodDelivery.Application.Authentication.Authentication;
using FoodDelivery.Infrastructure.Authentication.Settings;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication.Services
{
    public sealed class GoogleAuthValidator (IOptions<GoogleAuthSettings> _settings) : IGoogleAuthValidator
    {

        async Task<GoogleJsonWebSignature.Payload> IGoogleAuthValidator.ValidateTokenAsync(string idToken) => await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = [_settings.Value.ClientId,
            _settings.Value.WebClientId]
        });
    }
}
