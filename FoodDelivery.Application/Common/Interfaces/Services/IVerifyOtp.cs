namespace FoodDelivery.Application.Common.Interfaces.Services
{
    public interface IVerifyOtp
    {
        public Task<AuthenticationResult> VerifyOtpAsync(string email, string otp);
    }
}
