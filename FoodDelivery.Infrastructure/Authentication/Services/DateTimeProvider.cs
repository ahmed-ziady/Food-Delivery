using FoodDelivery.Application.Common.Interfaces.Services;

namespace FoodDelivery.Infrastructure.Authentication.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
