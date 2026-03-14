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

            // Map the relationship using the property, then tell EF which field to use
            var navigation = builder.Metadata.FindNavigation(nameof(User.Addresses))!;
            navigation.SetField("_addresses");
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(u => u.Addresses)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
       .ValueGeneratedNever();

            builder.Property(a => a.Street).IsRequired().HasMaxLength(50);
            builder.Property(a => a.AppartmentNumber).IsRequired().HasMaxLength(50);
            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(50);

            // Correct mapping: column name "Location" and SQL Server spatial type "geography"
            builder.Property(a => a.Location)
                   .HasColumnName("Location")
                   .HasColumnType("geography");

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Addresses) // strongly-typed; EF will use the field we set in UserConfiguration
                   .HasForeignKey(a => a.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }

    } }