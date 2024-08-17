using LSM.SsoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LSM.SsoService.Infrastructure.Persistence.Context;

public interface IDataContext
{
    public DbSet<User> Users { get; init; }
}