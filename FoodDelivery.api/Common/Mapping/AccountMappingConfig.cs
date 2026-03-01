using FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm;
using FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailRequest;
using FoodDelivery.Application.Account.Commands.Logout;
using FoodDelivery.Application.Account.Commands.UpdateProfile;
using FoodDelivery.Application.Account.Common;
using FoodDelivery.Contracts.Account;
using Mapster;

namespace FoodDelivery.api.Common.Mapping
{
    public class AccountMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AccountResult, AccountResponse>();
            config.NewConfig<(UpdateProfileRequest request, Guid userId), UpdateProfileCommand>()
           .Map(dest => dest.UserId, src => src.userId)
           .Map(dest => dest.FirstName, src => src.request.FirstName)
           .Map(dest => dest.LastName, src => src.request.LastName)
           .Map(dest => dest.Bio, src => src.request.Bio);
            config.NewConfig<(ChangeEmailRequest request, Guid userId), ChangeEmailConfirmCommand>()
              .Map(dest => dest.UserId, src => src.userId).Map(dest => dest.NewEmail, src => src.request.NewEmail);
            config.NewConfig<AccountResult, AccountResponse>();
        }
    }
}
