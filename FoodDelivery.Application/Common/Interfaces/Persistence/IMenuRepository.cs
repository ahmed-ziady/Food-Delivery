using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Interfaces.Persistence
{
    public interface IMenuRepository
    {
        Task<Menu?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<Menu?> GetByRestaurantIdAsync(Guid RestaurantId, CancellationToken cancellationToken);
        Task AddAsync(  Menu menu , CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
