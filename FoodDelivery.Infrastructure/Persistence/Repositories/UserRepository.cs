using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Persistence.Repositories
{
    public sealed class UserRepository(FoodDeliveryDbContext dbContext)
        : IUserRepository
    {
        public async Task<User?> GetByIdAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
