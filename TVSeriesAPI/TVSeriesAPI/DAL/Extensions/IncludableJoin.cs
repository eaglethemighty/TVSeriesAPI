using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;

namespace TVSeriesAPI.DAL.Extensions
{
    public interface IIncludableJoin<out TEntity, out TProperty> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>
    {
    }
    public class IncludableJoin<TEntity, TPreviousProperty> : IIncludableJoin<TEntity, TPreviousProperty>
    {
        private readonly IIncludableQueryable<TEntity, TPreviousProperty> _query;

        public IncludableJoin(IIncludableQueryable<TEntity, TPreviousProperty> query)
        {
            _query = query;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        public Expression Expression => _query.Expression;
        public Type ElementType => _query.ElementType;
        public IQueryProvider Provider => _query.Provider;

        internal IIncludableQueryable<TEntity, TPreviousProperty> GetQuery()
        {
            return _query;
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return _query.ToAsyncEnumerable().GetAsyncEnumerator();
        }
    }
}
