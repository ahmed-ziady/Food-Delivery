namespace FoodDelivery.Application.Common.Interfaces.Twilio
{
    public interface IMailingService 
    {
        public Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
