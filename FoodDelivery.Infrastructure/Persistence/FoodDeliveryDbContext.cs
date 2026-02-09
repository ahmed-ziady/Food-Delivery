using FoodDelivery.Domain.MenuAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodDelivery.Infrastructure.Persistence
{
    public class FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : DbContext(options)
    {

        public DbSet<Menu> Menus { get; set; }= null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodDeliveryDbContext).Assembly);
            //modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetProperties()).Where(p=>p.IsPrimaryKey()).ToList()

            //    .ForEach(p => p.ValueGenerated = ValueGenerated.Never);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType == typeof(Menu)) // add other aggregates here
                {
                    var key = entityType.FindPrimaryKey();
                    if (key is not null)
                    {
                        foreach (var property in key.Properties)
                        {
                            property.ValueGenerated = ValueGenerated.Never;
                        }
                    }
                }
            }
        }
    }
}
