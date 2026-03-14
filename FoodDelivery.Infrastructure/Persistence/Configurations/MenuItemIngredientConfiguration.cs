using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
    public sealed class MenuItemIngredientConfiguration : IEntityTypeConfiguration<MenuItemIngredient>
    {
        public void Configure(EntityTypeBuilder<MenuItemIngredient> builder)
        {
            builder.ToTable("MenuItemIngredients");

            builder.HasKey(mii => new { mii.MenuItemId, mii.IngredientId });

            builder.HasOne(mii => mii.MenuItem)
                   .WithMany(mi => mi.MenuItemIngredients)
                   .HasForeignKey(mii => mii.MenuItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mii => mii.Ingredient)
                   .WithMany(i => i.MenuItemIngredients)
                   .HasForeignKey(mii => mii.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
