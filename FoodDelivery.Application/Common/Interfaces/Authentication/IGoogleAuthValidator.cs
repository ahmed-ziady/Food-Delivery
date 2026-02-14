using Google.Apis.Auth;

namespace FoodDelivery.Application.Common.Interfaces.Authentication
{
    public interface IGoogleAuthValidator
    {
        Task<GoogleJsonWebSignature.Payload> ValidateTokenAsync(string idToken);
    }
}
