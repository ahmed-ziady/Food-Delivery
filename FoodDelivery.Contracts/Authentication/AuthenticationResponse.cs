namespace FoodDelivery.Contracts.Authentication;

public record AuthenticationResponse(
    string AccessToken,
    string RefreshToken
    );
