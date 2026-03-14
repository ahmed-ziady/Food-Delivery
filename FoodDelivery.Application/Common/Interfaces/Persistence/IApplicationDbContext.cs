using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Interfaces.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Address> Addresses { get; }
        Task<int>SaveChangesAsync(CancellationToken cancellationToken );
    }
}
