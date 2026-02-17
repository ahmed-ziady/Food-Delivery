namespace FoodDelivery.Application.Common.Exceptions
{
    public sealed class TooManyRequestsException : AppException
    {
        public TimeSpan? RetryAfter { get; }

        public TooManyRequestsException(string code, string message, TimeSpan? retryAfter = null)
            : base(code, message)
        {
            RetryAfter = retryAfter;
        }
    }
}
