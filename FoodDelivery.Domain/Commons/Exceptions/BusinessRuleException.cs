using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Commons.Exceptions
{
    public abstract class BusinessRuleException(string message, string code) : Exception(message)
    {
        public string Code { get; } = code;
    }
}
