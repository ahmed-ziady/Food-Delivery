namespace FoodDelivery.Application.Common.Exceptions
{
    public sealed class BadRequestException(string code, string message) : AppException(code, message)
    {
    }
}
