using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; } =null!;
    public string LastName { get; private set; }=null!;
    public string? ProfilePictureUrl { get; private set; } 
    public string? Bio { get; private set; } 
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
    public void UpdateProfile(string? firstName, string? lastName, string? bio)
    {
        if (firstName is not null)
            FirstName = firstName;

        if (lastName is not null)
            LastName = lastName;

        if (bio is not null)
            Bio = bio;
    }
    public void UpdateProfilePicture(string? profilePictureUrl)
    {
        if (profilePictureUrl is not null)
            ProfilePictureUrl = profilePictureUrl;
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
    }

}
