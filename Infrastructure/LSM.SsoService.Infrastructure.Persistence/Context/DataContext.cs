using System.Reflection;
using LSM.SsoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LSM.SsoService.Infrastructure.Persistence.Context;

internal sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options), IDataContext
{
    private static readonly Assembly PersistenceAssemblyMarker = typeof(DataContext).Assembly;
    
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(PersistenceAssemblyMarker);
        base.OnModelCreating(modelBuilder);
    }
}