using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.Enums;
using LSM.SsoService.Domain.ValueObjects;
using LSM.SsoService.Domain.ValueObjects.Tokens;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSM.SsoService.Infrastructure.Persistence.EntityConfigurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User).ToSnakeCase());

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName(nameof(User.Id).ToSnakeCase());

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName(nameof(User.Password).ToSnakeCase());

        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(
                email => email.ToString(),
                value => Email.Parse(value).Value)
            .HasColumnName(nameof(User.Email).ToSnakeCase());

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName(nameof(User.Name).ToSnakeCase());

        builder.Property(x => x.Surname)
            .IsRequired()
            .HasColumnName(nameof(User.Surname).ToSnakeCase());

        builder.Property(x => x.Patronymic)
            .HasDefaultValue(null)
            .HasColumnName(nameof(User.Patronymic).ToSnakeCase());

        builder.Property(x => x.Role)
            .IsRequired()
            .HasDefaultValue(Role.Reader)
            .HasColumnName(nameof(User.Role).ToSnakeCase());

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.ResetPasswordToken)
            .WithOne(x => x.User)
            .HasForeignKey<ResetPasswordToken>(x => x.UserId);
    }
}