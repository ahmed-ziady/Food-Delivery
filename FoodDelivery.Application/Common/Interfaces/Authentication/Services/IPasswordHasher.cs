
namespace FoodDelivery.Application.Common.Interfaces.Authentication.Services
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
}
