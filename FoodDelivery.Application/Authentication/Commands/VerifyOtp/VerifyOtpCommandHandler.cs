using FoodDelivery.Application.Common;
using FoodDelivery.Application.Common.Interfaces.Services;
using MediatR;


namespace FoodDelivery.Application.Authentication.Commands.VerifyOtp
{
    public sealed class VerifyOtpCommandHandler(IVerifyOtp verifyOtp) : IRequestHandler<VerifyOtpCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {

            var email = request.Email.Trim().ToLower();
            return  await verifyOtp.VerifyOtpAsync(email, request.Otp);

        }
    }
}
