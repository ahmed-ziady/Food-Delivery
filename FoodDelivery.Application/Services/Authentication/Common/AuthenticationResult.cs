using FoodDelivery.Domain.UserAggregate;

namespace FoodDelivery.Application.Services.Authentication.Common
{
    public record AuthenticationResult
        (
         User User,
            string AccessToken,
            string RefreshToken
        );
    
}