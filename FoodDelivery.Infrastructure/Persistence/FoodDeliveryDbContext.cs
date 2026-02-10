using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.MenuAggregate;
using FoodDelivery.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodDelivery.Infrastructure.Persistence;

public sealed class FoodDeliveryDbContext(
    DbContextOptions<FoodDeliveryDbContext> options) : DbContext(options)
{
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FoodDeliveryDbContext).Assembly);

        // Ensure all Aggregate Root IDs are NOT database-generated
        DisableKeyValueGeneration(modelBuilder);
    }

    private static void DisableKeyValueGeneration(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(IAggregateRootMarker).IsAssignableFrom(entityType.ClrType))
                continue;

            var key = entityType.FindPrimaryKey();
            if (key is null)
                continue;

            foreach (var property in key.Properties)
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}
