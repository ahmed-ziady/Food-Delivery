using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Common.Interfaces.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
    }

}
