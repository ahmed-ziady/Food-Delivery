namespace FoodDelivery.Contracts.Authentication
{
    public sealed record VerifyEmailRequest(string Email, string Otp);

}
