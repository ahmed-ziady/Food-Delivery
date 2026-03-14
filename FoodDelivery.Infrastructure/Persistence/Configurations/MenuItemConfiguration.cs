using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
    public sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Description).HasMaxLength(500);
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
            builder.Property(i => i.MenuSectionId).IsRequired();

            // Item -> MenuSection (explicit inverse)
            builder.HasOne(i => i.MenuSection)
                   .WithMany(s => s.Items)
                   .HasForeignKey(i => i.MenuSectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Pictures as value objects
            builder.OwnsMany(i => i.Pictures, picBuilder =>
            {
                picBuilder.ToTable("MenuItemPictures");
                picBuilder.WithOwner().HasForeignKey("MenuItemId");
                picBuilder.HasKey("MenuItemId", "Url");
                picBuilder.Property(p => p.Url).IsRequired().HasMaxLength(200);
            });

            // MenuItemIngredients (join entities) are mapped separately in MenuItemIngredientConfiguration.
            // Use backing-field access for the join collection
            builder.Navigation(i => i.MenuItemIngredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
