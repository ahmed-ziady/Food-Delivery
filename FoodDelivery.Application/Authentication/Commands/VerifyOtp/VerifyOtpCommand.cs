using FoodDelivery.Application.Common;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.VerifyOtp
{
    public record VerifyOtpCommand(string Email, string Otp) : IRequest<AuthenticationResult>;

}
