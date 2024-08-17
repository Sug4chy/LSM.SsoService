using LSM.SsoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSM.SsoService.Infrastructure.Persistence.EntityConfigurations;

public sealed class SessionEntityConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("session");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("id");
        
        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasColumnName("start_date");
        
        builder.Property(x => x.EndDate)
            .HasColumnName("end_date");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.UserId);
        builder.Property(x => x.UserId)
            .HasDefaultValue(null)
            .HasColumnName("user_id");
    }
}