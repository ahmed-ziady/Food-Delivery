using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(256);
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

            // Do not configure the Menu relationship here to avoid duplicate/conflicting mapping.
            // The one-to-one Menu <-> User mapping is configured in MenuConfiguration.
        }
    }

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

            // One-to-one: Menu (dependent) -> User (principal)
            builder.HasOne(m => m.Restaurant)
                   .WithOne(u => u.Menu)
                   .HasForeignKey<Menu>(m => m.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Menu -> Sections (entity collection)
            builder.HasMany(m => m.Sections)
                   .WithOne(s => s.Menu)
                   .HasForeignKey(s => s.MenuId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Use backing field access for the sections collection
            builder.Navigation(m => m.Sections)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .AutoInclude();
        }
    }

    public sealed class MenuSectionConfiguration : IEntityTypeConfiguration<MenuSection>
    {
        public void Configure(EntityTypeBuilder<MenuSection> builder)
        {
            builder.ToTable("MenuSections");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.MenuId).IsRequired();

            // Section -> Menu (explicit inverse)
            builder.HasOne(s => s.Menu)
                   .WithMany(m => m.Sections)
                   .HasForeignKey(s => s.MenuId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Section -> Items (entity collection)
            builder.HasMany(s => s.Items)
                   .WithOne(i => i.MenuSection)
                   .HasForeignKey(i => i.MenuSectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Backing field access for items
            builder.Navigation(s => s.Items)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .AutoInclude();
        }
    }

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
