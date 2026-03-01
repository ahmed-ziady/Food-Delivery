using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Commons.Exceptions;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
    public sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.RestaurantId).IsRequired();
            builder.HasIndex(x => x.RestaurantId).IsUnique();
            builder.HasOne<User>()
                   .WithOne()
                   .HasForeignKey<Menu>(m => m.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Sections
            builder.OwnsMany<MenuSection>("_sections", sectionBuilder =>
            {
                sectionBuilder.ToTable("MenuSections");
                sectionBuilder.WithOwner().HasForeignKey("MenuId");

                sectionBuilder.HasKey(x => x.Id);
                sectionBuilder.Property(x => x.Id).ValueGeneratedNever();
                sectionBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

                // Map _items in MenuSection
                sectionBuilder.OwnsMany<MenuItem>("_items", itemBuilder =>
                {
                    itemBuilder.ToTable("MenuItems");
                    itemBuilder.WithOwner().HasForeignKey("MenuSectionId");

                    itemBuilder.HasKey(x => x.Id);
                    itemBuilder.Property(x => x.Id).ValueGeneratedNever();
                    itemBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
                    itemBuilder.Property(x => x.Description).HasMaxLength(500);
                    itemBuilder.Property(x => x.Price).HasColumnType("decimal(18,2)");

                    // Ingredients
                    itemBuilder.OwnsMany<Ingredient>("_ingredients", i =>
                    {
                        i.ToTable("MenuItemIngredients");
                        i.WithOwner().HasForeignKey("MenuItemId");
                        i.Property(x => x.Name).IsRequired().HasMaxLength(100);
                        i.HasKey("MenuItemId", "Name"); // composite key
                    });
                    itemBuilder.Ignore(i => i.Ingredients); // Ignore the public property
                    itemBuilder.Navigation("_ingredients").UsePropertyAccessMode(PropertyAccessMode.Field);

                    // Pictures
                    itemBuilder.OwnsMany<Picture>("_pictures", p =>
                    {
                        p.ToTable("MenuItemPictures");
                        p.WithOwner().HasForeignKey("MenuItemId");
                        p.Property(x => x.Url).IsRequired().HasMaxLength(200);
                        p.HasKey("MenuItemId", "Url");
                    });
                    itemBuilder.Ignore(i => i.Pictures); // Ignore the public property
                    itemBuilder.Navigation("_pictures").UsePropertyAccessMode(PropertyAccessMode.Field);
                });
                sectionBuilder.Navigation("_items").UsePropertyAccessMode(PropertyAccessMode.Field);
                sectionBuilder.Ignore(s => s.Items); // Ignore the public property
            });

            // IMPORTANT FIX: Ignore the public property so EF won't try to map it
            builder.Ignore(m => m.Sections);

            // Map backing field
            builder.Navigation("_sections").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation("_sections").AutoInclude();
        }
    }
}