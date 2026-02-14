using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations;

public sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .HasMaxLength(400);

        builder.Property(x => x.Price)
               .HasPrecision(18, 2);

        builder.HasOne<MenuSection>()
               .WithMany(x => x.MenuItems)
               .HasForeignKey(x => x.MenuSectionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.MenuSectionId);
    }
}
