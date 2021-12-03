using System.Linq.Expressions;
using TVSeriesAPI.DAL.Extensions;

namespace TVSeriesAPI.DAL.Repositories.Interfaces
{
    public interface IRepositoryJoin<TEntity> where TEntity : class
    {
        IIncludableJoin<TEntity, TProperty> Join<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity obj);
        Task DeleteAsync(TEntity obj);
        Task UpdateAsync(TEntity obj);
        Task<bool> SaveChanges();
    }
}
