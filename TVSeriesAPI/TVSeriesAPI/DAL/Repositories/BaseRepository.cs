using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TVSeriesAPI.DAL.Extensions;
using TVSeriesAPI.DAL.Repositories.Interfaces;

namespace TVSeriesAPI.DAL.Repositories
{ 
    public abstract class BaseRepository<TEntity> : IRepositoryJoin<TEntity>
    where TEntity : class, new()
    {
        private TVSeriesDbContext dataContext { get; set; }
        protected BaseRepository(TVSeriesDbContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public virtual IIncludableJoin<TEntity, TProperty> Join<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty)
        {
            return ((IQueryable<TEntity>)dataContext.Set<TEntity>()).Join(navigationProperty);
        }
        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.Run(() => (IQueryable<TEntity>)dataContext.Set<TEntity>());
        }
        public Task AddAsync(TEntity obj)
        {
            return Task.Run(() => dataContext.Set<TEntity>().AddAsync(obj));
        }

        public Task DeleteAsync(TEntity obj)
        {
            return Task.Run(() => dataContext.Set<TEntity>().Remove(obj));
        }

        public Task<bool> SaveChanges()
        {
            return Task.Run(() => dataContext.SaveChanges() != 0);
        }

        public Task UpdateAsync(TEntity obj)
        {
            return Task.Run(() => dataContext.Set<TEntity>().Update(obj));
        }

        public async Task<IList<TEntity>> ToListAsync(IQueryable<TEntity> query)
        {
            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}