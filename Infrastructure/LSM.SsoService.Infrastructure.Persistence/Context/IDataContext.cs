using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.ValueObjects.Tokens;
using Microsoft.EntityFrameworkCore;

namespace LSM.SsoService.Infrastructure.Persistence.Context;

public interface IDataContext
{
    public DbSet<User> Users { get; }
    public DbSet<ResetPasswordToken> ResetPasswordTokens { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}