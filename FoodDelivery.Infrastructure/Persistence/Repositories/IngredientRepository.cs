using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Persistence.Repositories
{
    public sealed class IngredientRepository(FoodDeliveryDbContext _dbContext) : IIngredientRepository
    {
        public async Task<IEnumerable<Ingredient>> GetAllAsync(CancellationToken cancellationToken   )
           => await _dbContext.Ingredients.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Ingredient?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _dbContext.Ingredients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id , cancellationToken);

        public async Task AddAsync(Ingredient ingredient , CancellationToken cancellationToken)
            => await _dbContext.Ingredients.AddAsync(ingredient , cancellationToken);
        public void Remove(Ingredient ingredient)
            => _dbContext.Ingredients.Remove(ingredient);

        public Task SaveChanagesAsync(CancellationToken cancellationToken)
        {
          return _dbContext.SaveChangesAsync(cancellationToken);

        }
        public async Task<List<Ingredient>> GetByIdsAsync(List<Guid> ids, CancellationToken ct)
        {
            return await _dbContext.Ingredients.Where(i => ids.Contains(i.Id)).ToListAsync(ct);
        }
    }
}
