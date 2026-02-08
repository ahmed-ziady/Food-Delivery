using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace FoodDelivery.Infrastructure.Persistence
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private static readonly List<RefreshToken> _refreshTokens = [];
        public Task AddAsync(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
            return Task.CompletedTask;
        }

        public Task<RefreshToken?> GetByTokenAsync(string token)
        {
            var refreshToken = _refreshTokens
                .FirstOrDefault(x => x.Token == token);

            return Task.FromResult(refreshToken);
        }

        public Task UpdateAsync(RefreshToken refreshToken)
        {
            // مفيش حاجة نعملها فعلياً
            // لأننا بنعدل reference في memory
            return Task.CompletedTask;
        }


    }
}
