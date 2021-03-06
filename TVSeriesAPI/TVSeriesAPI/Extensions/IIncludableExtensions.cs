using TVSeriesAPI.DAL.Extensions;

namespace TVSeriesAPI.IIncludableExtensions
{
    public static class IIncludableExtensions
    {

        public static async Task<IList<TEntity>> ToListAsyncCustom<TEntity, TProperty>(this IIncludableJoin<TEntity, TProperty> query)
        {
            return await query.ToListAsync();
        }
    }
}
