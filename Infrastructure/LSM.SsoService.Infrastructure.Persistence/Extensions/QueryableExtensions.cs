using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace LSM.SsoService.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<Maybe<TEntity>> TryFirstAsync<TEntity>(
        this IQueryable<TEntity> source,
        CancellationToken ct = default)
        where TEntity : class
    {
        var result = await source.FirstOrDefaultAsync(ct);
        return result ?? Maybe<TEntity>.None;
    }

    public static async Task<Maybe<TEntity>> TryFirstAsync<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var result = await source.FirstOrDefaultAsync(predicate, ct);
        return result ?? Maybe<TEntity>.None;
    }
}