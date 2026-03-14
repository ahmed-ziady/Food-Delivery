using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{

    public sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.RestaurantId).IsRequired();
            builder.HasIndex(m => m.RestaurantId).IsUnique();

            builder.HasOne(m => m.Restaurant)
                   .WithOne(u => u.Menu)
                   .HasForeignKey<Menu>(m => m.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Sections)
                   .WithOne(s => s.Menu)
                   .HasForeignKey(s => s.MenuId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(m => m.Sections)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .AutoInclude();
        }
    }
}
