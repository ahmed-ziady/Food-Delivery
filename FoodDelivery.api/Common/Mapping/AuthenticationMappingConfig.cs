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

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .MapWith(src => new AuthenticationResponse(
                src.User.Id.Value,
                src.User.FirstName,
                src.User.LastName,
                src.User.Email.Value,
                src.AccessToken,
                src.RefreshToken
            ));

        }
    }
    
}
