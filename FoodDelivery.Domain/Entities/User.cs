using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; } =null!;
    public string LastName { get; private set; }=null!;

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }

    private User() { }

    public User(string firstName, string lastName, string email,string phoneNumber)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = email;
        PhoneNumber=phoneNumber;
    }

    public void IssueRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiry = expiry;
    }
}
