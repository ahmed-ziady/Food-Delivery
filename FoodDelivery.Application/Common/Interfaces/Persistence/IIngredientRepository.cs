using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Interfaces.Persistence
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllAsync(CancellationToken cancellationToken);
        Task<Ingredient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Ingredient ingredient, CancellationToken cancellationToken);
        Task<List<Ingredient>> GetByIdsAsync(List<Guid> ids, CancellationToken ct);
        void Remove(Ingredient ingredient);
        Task SaveChanagesAsync(CancellationToken cancellationToken);
    }
}
