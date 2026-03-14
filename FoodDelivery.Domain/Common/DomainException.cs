using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Commons
{
    public class DomainException(string message) : Exception(message)
    {
    }
}
