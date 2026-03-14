using FoodDelivery.Application.Account.Commands.Address.Comman;
using FoodDelivery.Application.Account.Commands.Address.Commands.SetAddress;
using FoodDelivery.Application.Account.Commands.Address.Commands.UpdateAddress;
using FoodDelivery.Contracts.Account;
using Mapster;

namespace FoodDelivery.api.Common.Mapping
{
    public class AddressMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddressResult, AddressResponse>();
            config.NewConfig<(SetAddressRequest request, Guid userId), SetAddressCommand>()
                .Map(dest => dest.UserId, src => src.userId)
               .Map(dest => dest.Street, src => src.request.Street)
                .Map(dest => dest.PostalCode, src => src.request.PostalCode)
                .Map(dest => dest.AppartmentNumber, src => src.request.AppartmentNumber)
                .Map(dest => dest.Lat, src => src.request.Lat)
                .Map(dest => dest.Lng, src => src.request.Lng)
                .Map(dest => dest.Label, src => src.request.Label);

            config.NewConfig<UpdateAddressRequest, UpdateAddressCommand>();
        }
    }
}
