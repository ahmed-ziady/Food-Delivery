namespace FoodDelivery.Application.Account.Common
{
    public record AccountResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string? PhoneNumber,
        string? Bio,
        string? ProfilePictureUrl,
        bool EmailConfirmed,
        bool PhoneNumberConfirmed
    );
}
