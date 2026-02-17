using Google.Apis.Auth;

namespace FoodDelivery.Application.Authentication.Authentication
{
    public interface IGoogleAuthValidator
    {
        Task<GoogleJsonWebSignature.Payload> ValidateTokenAsync(string idToken);
    }
}
