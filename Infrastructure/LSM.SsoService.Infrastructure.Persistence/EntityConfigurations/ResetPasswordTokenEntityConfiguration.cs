using LSM.SsoService.Domain.ValueObjects.Tokens;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSM.SsoService.Infrastructure.Persistence.EntityConfigurations;

public sealed class ResetPasswordTokenEntityConfiguration : IEntityTypeConfiguration<ResetPasswordToken>
{
    public void Configure(EntityTypeBuilder<ResetPasswordToken> builder)
    {
        builder.ToTable(nameof(ResetPasswordToken).ToSnakeCase());

        builder.HasKey(
            nameof(ResetPasswordToken.TokenExpiryDate),
            nameof(ResetPasswordToken.UserId)
        );
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasColumnName(nameof(ResetPasswordToken.Token).ToSnakeCase());
        
        builder.Property(x => x.TokenExpiryDate)
            .IsRequired()
            .HasColumnName(nameof(ResetPasswordToken.TokenExpiryDate).ToSnakeCase());

        builder.HasOne(x => x.User)
            .WithOne(x => x.ResetPasswordToken)
            .HasForeignKey<ResetPasswordToken>(x => x.UserId);
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName(nameof(ResetPasswordToken.UserId).ToSnakeCase());
    }
}