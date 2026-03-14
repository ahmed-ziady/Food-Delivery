using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Infrastructure.Identity
{
    public sealed class UserService(FoodDeliveryDbContext dbContext, UserManager<User> userManager)
        : IUserService
    {
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task <IdentityResult>CreateAsync(User user, string password)
        {
         return await userManager.CreateAsync(user, password);
        }


        public async Task<string> GenerateEmailTokenAsync(User user)
        {
            return await userManager.GenerateTwoFactorTokenAsync(user,TokenOptions.DefaultEmailProvider);
        }

        public async Task<User?> GetByEmailAsync(string Email)
        {
            return await userManager.FindByEmailAsync(Email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user)
        {
            await userManager.UpdateAsync(user);

        }

        public Task<IdentityResult> DeleteAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
