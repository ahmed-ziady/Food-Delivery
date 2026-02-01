using FoodDelivery.Application.Common.Interfaces.Authentication.Services;

namespace FoodDelivery.Infrastructure.Authentication.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
