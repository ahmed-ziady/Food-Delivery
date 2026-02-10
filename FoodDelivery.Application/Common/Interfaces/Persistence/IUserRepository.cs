using FoodDelivery.Domain.UserAggregate;
using FoodDelivery.Domain.UserAggregate.ValueObjects;

namespace FoodDelivery.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(
        UserId userId,
        CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken);

    Task<User?> GetByRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken);

    Task AddAsync(
        User user,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        User user,
        CancellationToken cancellationToken);
}
