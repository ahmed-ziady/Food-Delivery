using FoodDelivery.Domain.HostAggregate.ValueObjects;
using FoodDelivery.Domain.MenuAggregate;
using FoodDelivery.Domain.MenuAggregate.Entities;
using FoodDelivery.Domain.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations
{
    public class MenuConfigurations : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            ConfigureMenusTable(builder);
            ConfigureMenuSectionsTable(builder);
            ConfigureMenuDinnerIdsTable(builder);
            ConfigureMenuReviewIdstable(builder);
        }

        private static void ConfigureMenuReviewIdstable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.MenuReviewIds, di =>
            {
                di.ToTable("MenuReviewrIds");
                di.WithOwner().HasForeignKey("MenuId");
                di.HasKey("Id");
                di.Property(d => d.Value)
                    .HasColumnName("ReviewId");
            });
            builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.DinnerIds, di =>
            {
                di.ToTable("MenuDinnerIds");
                di.WithOwner().HasForeignKey("MenuId");
                di.HasKey("Id");
                di.Property(d => d.Value)
                    .HasColumnName("DinnerId");
            });
             builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.Sections, ms =>
            {
                ms.ToTable("MenuSections");
                ms.WithOwner().HasForeignKey("MenuId");
                ms.HasKey("Id", "MenuId");
                ms.Property(s => s.Id)
                 .HasConversion(
                 id => id.Value,
                 value => MenuSectionId.From(value)
                 );
                ms.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                ms.Property(s => s.Description)
                    .HasMaxLength(400);

                ms.OwnsMany(s => s.MenuItems, mib =>
                {
                    mib.ToTable("MenuItems");
                    mib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");
                    mib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");
                    mib.Property(i => i.Id)
                    .HasColumnName("MenuItemsId")
                        .HasConversion(
                            id => id.Value,
                            value => MenuItemId.From(value)
                        );
                    mib.Property(i => i.Name)
                        .IsRequired()
                        .HasMaxLength(100);
                    mib.Property(i => i.Description)
                        .HasMaxLength(400);
                    mib.OwnsOne(i => i.Price, p =>
                    {
                        p.Property(pr => pr.Amount).HasColumnName("Price");
                    });
                    mib.OwnsOne(s => s.AverageRating, ar =>
                    {
                        ar.Property(p => p.Value).HasColumnName("AverageRating");
                        ar.Property(p => p.NumberOfRatings).HasColumnName("RatingCount");
                    });


                });
                
                ms.Navigation(s => s.MenuItems)!
                    .Metadata.SetField("_menuItems");
                ms.Navigation(s => s.MenuItems)!
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
            builder.Metadata.FindNavigation(nameof(Menu.Sections))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }

        private static void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasConversion(
                id => id.Value,
                value => MenuId.From(value)
            );
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Description)
                .HasMaxLength(400);

            builder.OwnsOne(m => m.AverageRating , ar=>
            {
                ar.Property(p => p.Value).HasColumnName("AverageRating");
                ar.Property(p => p.NumberOfRatings).HasColumnName("RatingCount");
            });

            builder.Property(m => m.HostId)
                .HasConversion(
                id => id.Value,
                value => HostId.From(value)
                );
        }
    }
}
