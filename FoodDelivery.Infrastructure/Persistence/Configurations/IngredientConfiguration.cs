using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
    public sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredients");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.ImageUrl).HasMaxLength(200);
            builder.Property(i => i.Type).IsRequired();

            // Ingredient -> MenuItemIngredients (join)
            builder.HasMany(i => i.MenuItemIngredients)
                   .WithOne(mii => mii.Ingredient)
                   .HasForeignKey(mii => mii.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(i => i.MenuItemIngredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
