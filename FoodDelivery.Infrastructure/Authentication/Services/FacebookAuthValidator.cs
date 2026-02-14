using FoodDelivery.Application.Common;
using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.Infrastructure.Authentication.Services
{
    public class FacebookAuthValidator(IOptions<FacebookAuthSettings> _settings,HttpClient httpClient) : IFacebookAuthValidator
    {

public async Task<FacebookUserInfo> ValidateTokenAsync(string accessToken)
    {
        // 1️⃣ Get App Access Token
        var appTokenResponse = await httpClient.GetAsync(
            $"https://graph.facebook.com/oauth/access_token?client_id={_settings.Value.AppId}&client_secret={_settings.Value.AppSecret}&grant_type=client_credentials");

        if (!appTokenResponse.IsSuccessStatusCode)
            throw new Exception("Failed to retrieve Facebook app access token.");

        var appTokenJson = await appTokenResponse.Content.ReadAsStringAsync();
        var appAccessToken = JsonDocument.Parse(appTokenJson)
            .RootElement.GetProperty("access_token").GetString();

        // 2️⃣ Validate Token
        var debugResponse = await httpClient.GetAsync(
            $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appAccessToken}");

        if (!debugResponse.IsSuccessStatusCode)
            throw new Exception("Failed to validate Facebook access token.");

        var debugJson = await debugResponse.Content.ReadAsStringAsync();
        var debugData = JsonDocument.Parse(debugJson)
            .RootElement.GetProperty("data");

        if (!debugData.GetProperty("is_valid").GetBoolean())
            throw new UnauthorizedAccessException("Invalid Facebook access token.");

        // 3️⃣ Get User Info
        var userResponse = await httpClient.GetAsync(
            $"https://graph.facebook.com/me?fields=id,first_name,last_name,name,email,picture.type(large)&access_token={accessToken}");

        if (!userResponse.IsSuccessStatusCode)
            throw new Exception("Failed to retrieve Facebook user info.");

        var userJson = await userResponse.Content.ReadAsStringAsync();
        var root = JsonDocument.Parse(userJson).RootElement;

        var pictureUrl = root.GetProperty("picture")
            .GetProperty("data")
            .GetProperty("url")
            .GetString();

        return new FacebookUserInfo
        {
            FirstName = root.GetProperty("first_name").GetString() ?? "",
            LastName = root.GetProperty("last_name").GetString() ?? "",
            Email = root.TryGetProperty("email", out var emailProp)
                ? emailProp.GetString() ?? ""
                : "",
            PictureUrl = pictureUrl ?? "",
            ProviderId = root.GetProperty("id").GetString() ?? ""


        };
    }
}
}
