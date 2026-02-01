using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Contracts.Authentication;
using Mapster;

namespace FoodDelivery.api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginCommand>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>().Map(dest => dest, src => src.User);
        }
    }
}
