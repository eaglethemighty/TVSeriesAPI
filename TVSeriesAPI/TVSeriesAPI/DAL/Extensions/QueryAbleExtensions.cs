using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TVSeriesAPI.DAL.Extensions
{
    public static class RepositoryExtensions
    {
        public static IIncludableJoin<TEntity, TProperty> Join<TEntity, TProperty>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, TProperty>> propToExpand)
            where TEntity : class
        {
            return new IncludableJoin<TEntity, TProperty>(query.Include(propToExpand));
        }

        public static IIncludableJoin<TEntity, TProperty> ThenJoin<TEntity, TPreviousProperty, TProperty>(
             this IIncludableJoin<TEntity, TPreviousProperty> query,
             Expression<Func<TPreviousProperty, TProperty>> propToExpand)
            where TEntity : class
        {
            IIncludableQueryable<TEntity, TPreviousProperty> queryable = ((IncludableJoin<TEntity, TPreviousProperty>)query).GetQuery();
            return new IncludableJoin<TEntity, TProperty>(queryable.ThenInclude(propToExpand));
        }

        public static IIncludableJoin<TEntity, TProperty> ThenJoin<TEntity, TPreviousProperty, TProperty>(
            this IIncludableJoin<TEntity, IList<TPreviousProperty>> query,
            Expression<Func<TPreviousProperty, TProperty>> propToExpand)
            where TEntity : class
        {
            var queryable = ((IncludableJoin<TEntity, IList<TPreviousProperty>>)query).GetQuery();
            var include = queryable.ThenInclude(propToExpand);
            return new IncludableJoin<TEntity, TProperty>(include);
        }
        public static IIncludableJoin<TEntity, TProperty> ThenJoin<TEntity, TPreviousProperty, TProperty>(
            this IIncludableJoin<TEntity, ICollection<TPreviousProperty>> query,
            Expression<Func<TPreviousProperty, TProperty>> propToExpand)
            where TEntity : class
        {
            var queryable = ((IncludableJoin<TEntity, ICollection<TPreviousProperty>>)query).GetQuery();
            var include = queryable.ThenInclude(propToExpand);
            return new IncludableJoin<TEntity, TProperty>(include);
        }
        public static async Task<TEntity> FirstOrDefaultAsyncCustom<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate)
        {
            return await query.FirstOrDefaultAsync(predicate);
        }

        public static async Task<IList<TEntity>> ToListAsyncCustom<TEntity>(this IQueryable<TEntity> query)
        {
            return await query.ToListAsync();
        }
    }
}
