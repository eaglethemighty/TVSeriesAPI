using System.Linq.Expressions;
using TVSeriesAPI.DAL.Extensions;

namespace TVSeriesAPI.IIncludableEntensions
{
    public static class IIncludableExtensions
    {

        public static async Task<IList<TEntity>> ToListAsyncCustom<TEntity, TProperty>(this IIncludableJoin<TEntity, TProperty> query)
        {
            return await query.ToListAsync();
        }

        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate)
        {
            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
