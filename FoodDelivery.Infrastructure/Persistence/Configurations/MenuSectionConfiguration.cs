using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations;

public sealed class MenuSectionConfiguration : IEntityTypeConfiguration<MenuSection>
{
    public void Configure(EntityTypeBuilder<MenuSection> builder)
    {
        builder.ToTable("MenuSections");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .HasMaxLength(400);

        builder.HasOne<Menu>()
               .WithMany(x => x.Sections)
               .HasForeignKey(x => x.MenuId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.MenuId);
    }
}
