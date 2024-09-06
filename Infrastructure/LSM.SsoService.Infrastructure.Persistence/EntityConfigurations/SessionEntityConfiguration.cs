using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSM.SsoService.Infrastructure.Persistence.EntityConfigurations;

public sealed class SessionEntityConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable(nameof(Session).ToSnakeCase());
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName(nameof(Session.Id).ToSnakeCase());
        
        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasColumnName(nameof(Session.StartDate).ToSnakeCase());
        
        builder.Property(x => x.EndDate)
            .HasColumnName(nameof(Session.EndDate).ToSnakeCase());

        builder.HasOne(x => x.User)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.UserId);
        builder.Property(x => x.UserId)
            .HasDefaultValue(null)
            .HasColumnName(nameof(Session.UserId).ToSnakeCase());
    }
}