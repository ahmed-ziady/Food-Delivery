namespace FoodDelivery.Application.Common
{
    public record AuthenticationResult
        (
            string AccessToken,
            string RefreshToken
        );

}