using LSM.SsoService.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSM.SsoService.Infrastructure.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> OwnsEmail<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IHasEmail
        => builder.OwnsOne(e => e.Email, navigationBuilder =>
        {
            navigationBuilder.Property(email => email.Value)
                .HasColumnName("email");
            navigationBuilder.HasIndex(email => email.Value)
                .IsUnique();
        });
}