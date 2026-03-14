using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Persistence.Repositories;

public class MenuRepository(FoodDeliveryDbContext db) : IMenuRepository
{
    public async Task AddAsync(Menu menu, CancellationToken cancellationToken)
    {
        await db.Menus.AddAsync(menu, cancellationToken);
    }

    public async Task<Menu?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await db.Menus
            .Include(m => m.Sections)
                .ThenInclude(s => s.Items)
                    .ThenInclude(i => i.MenuItemIngredients)
                        .ThenInclude(mii => mii.Ingredient)
            .Include(m => m.Sections)
                .ThenInclude(s => s.Items)
                    .ThenInclude(i => i.Pictures)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken); 
    }

    public async Task<Menu?> GetByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken)
    {
        return await db.Menus
            .Include(m => m.Sections)
                .ThenInclude(s => s.Items)
                    .ThenInclude(i => i.MenuItemIngredients)
                        .ThenInclude(mii => mii.Ingredient)
            .Include(m => m.Sections)
                .ThenInclude(s => s.Items)
                    .ThenInclude(i => i.Pictures)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.RestaurantId == restaurantId, cancellationToken);
    }



    public Task DeleteAsync(Menu menu)
    {
        db.Menus.Remove(menu);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return db.SaveChangesAsync(cancellationToken);
    }
}
