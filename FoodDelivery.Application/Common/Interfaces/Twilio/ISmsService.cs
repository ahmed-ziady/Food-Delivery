using Twilio.Rest.Api.V2010.Account;

namespace FoodDelivery.Application.Common.Interfaces.Twilio
{
    public interface ISmsService
    {
        public Task<MessageResource> SendAsync(string phoneNumber, string message);


    }
}
