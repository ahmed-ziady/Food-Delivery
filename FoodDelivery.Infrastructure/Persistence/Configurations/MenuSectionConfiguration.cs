using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
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
}
