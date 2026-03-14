namespace FoodDelivery.Domain.Commons.Exceptions
{
    public class PriceMustBePositiveException : BusinessRuleException
    {
        public PriceMustBePositiveException(decimal price)
            : base($"Price '{price}' must be a positive value.",
                   "PRICE_MUST_BE_POSITIVE")
        {
        }
    }
}
