using FoodDelivery.Domain.UserAggregate;
using FoodDelivery.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureRefreshTokensTable(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.From(value));

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(u => u.PhoneNumber, phone =>
        {
            phone.Property(p => p.Value)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(20);
        });

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value));

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasConversion(
                hash => hash.Value,
                value => PasswordHash.Create(value));

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(
                role => role.Name,
                value => UserRole.From(value));

        builder.Property(u => u.CreatedAtUtc)
            .IsRequired();

        builder.Property(u => u.UpdatedAtUtc);
    }

    private static void ConfigureRefreshTokensTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.RefreshTokens, rt =>
        {
            rt.ToTable("RefreshTokens");

            rt.WithOwner()
              .HasForeignKey("UserId");

            // 🔑 PK
            rt.HasKey("Id");

            rt.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RefreshTokenId.From(value));

            rt.Property(r => r.Token)
                .IsRequired()
                .HasMaxLength(256);

            rt.Property(r => r.CreatedAtUtc)
                .IsRequired();

            rt.Property(r => r.ExpiresAtUtc)
                .IsRequired();

            rt.Property(r => r.RevokedAtUtc);

            rt.Property(r => r.RevokedReason)
                .HasMaxLength(200);

            rt.Property(r => r.ReplacedByToken)
                .HasMaxLength(256);
        });

        builder.Metadata
            .FindNavigation(nameof(User.RefreshTokens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
