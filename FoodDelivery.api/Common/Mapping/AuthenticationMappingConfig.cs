using FoodDelivery.Application.Authentication.Commands.FacebookLogin;
using FoodDelivery.Application.Authentication.Commands.GoogleLogin;
using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Refresh;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Application.Authentication.Commands.ResendVerificationCode;
using FoodDelivery.Application.Authentication.Commands.ResetPassword;
using FoodDelivery.Application.Authentication.Commands.VerifyOtp;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Contracts.Authentication;
using FoodDelivery.Contracts.RefreshToken;
using Mapster;

namespace FoodDelivery.api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginCommand>();
            config.NewConfig<VerifyEmailRequest, VerifyOtpCommand>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>();
            config.NewConfig<RefreshTokenRequest, RefreshCommand>();
            config.NewConfig<GoogleLoginRequest, GoogleLoginCommand>();
            config.NewConfig<FacebookLoginRequest, FacebookLoginCommand>();
            config.NewConfig<ResendVerificatonCodeRequest, ResendVerificationCodeCommand>();
            config.NewConfig<ResetPasswordRequest, ResetPasswordCommand>();

        }
    }
    
}
