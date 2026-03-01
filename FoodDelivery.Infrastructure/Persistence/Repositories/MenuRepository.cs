using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using Google.Apis.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Persistence.Repositories
{
    public class MenuRepository(FoodDeliveryDbContext _dbContext) : IMenuRepository
    {
        public async Task AddAsync(Menu menu, CancellationToken cancellationToken)
        {
           await _dbContext.Menus.AddAsync(menu, cancellationToken);
        }
        public async Task<Menu?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Menus
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
        public async Task<Menu?> GetByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken)
        {
            return await _dbContext.Menus
                .AsSplitQuery()
                .FirstOrDefaultAsync(m => m.RestaurantId == restaurantId, cancellationToken);
        }
        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
           return _dbContext.SaveChangesAsync(cancellationToken);
        }
        public Task UpdateAsync(Menu menu, CancellationToken cancellationToken)
        {
          _dbContext.Menus.Update(menu);
            return Task.CompletedTask;
        }
    }
}
