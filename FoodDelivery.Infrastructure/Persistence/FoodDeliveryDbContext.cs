using FoodDelivery.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Persistence;

public sealed class FoodDeliveryDbContext(
    DbContextOptions<FoodDeliveryDbContext> options)
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{

    // Your business DbSets
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuSection> MenuSections { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FoodDeliveryDbContext).Assembly);
    }
}
