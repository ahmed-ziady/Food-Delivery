using FoodDelivery.Domain.Commons.Exceptions;

namespace FoodDelivery.Domain.Common.Exceptions
{
    public class TooManyAddressException(int maxAllowed) : BusinessRuleException($"A Account  cannot have more than {maxAllowed} addresses.",
               "TOO_MANY_ADDRESSESS")
    {
    }
}
