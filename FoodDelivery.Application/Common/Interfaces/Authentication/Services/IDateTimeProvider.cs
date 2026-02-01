using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Interfaces.Authentication.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
