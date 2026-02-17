using FoodDelivery.Application.Account.Common;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?>  GetByIdAsync(Guid userId , CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
