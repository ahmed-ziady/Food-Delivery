using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.UserAggregate;
using FoodDelivery.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(FoodDeliveryDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        User user,
        CancellationToken cancellationToken)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(
                u => u.Id == userId,
                cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(
                u => u.Email == email,
                cancellationToken);
    }

    public async Task<User?> GetByRefreshTokenAsync(
     string refreshToken,
     CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u =>
                u.RefreshTokens.Any(rt => rt.Token == refreshToken),
                cancellationToken);
    }

}
