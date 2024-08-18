using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.Enums;
using LSM.SsoService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSM.SsoService.Infrastructure.Persistence.EntityConfigurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("id");

        builder.HasIndex(x => x.Username)
            .IsUnique();
        builder.Property(x => x.Username)
            .IsRequired()
            .HasColumnName("username");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("password");
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(
                email => email.ToString(),
                value => Email.Parse(value).Value)
            .HasColumnName("email");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(x => x.Surname)
            .IsRequired()
            .HasColumnName("surname");

        builder.Property(x => x.Patronymic)
            .HasDefaultValue(null)
            .HasColumnName("patronymic");

        builder.Property(x => x.Role)
            .IsRequired()
            .HasDefaultValue(Role.Reader)
            .HasColumnName("role");

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}