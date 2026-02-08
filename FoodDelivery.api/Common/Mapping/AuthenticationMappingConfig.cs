using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Register;
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
            config.NewConfig<AuthenticationResult, AuthenticationResponse>().Map(dest => dest.AccessToken, src => src.AccessToken).Map(dest => dest.RefreshToken, src => src.RefreshToken).Map(dest => dest.Id, src => src.User.Id).Map(dest => dest.FirstName, src => src.User.FirstName).Map(dest => dest.LastName, src => src.User.LastName).Map(dest => dest, src => src) .Map(dest=>dest.Email, src=>src.User.Email);
        }
    }
}
