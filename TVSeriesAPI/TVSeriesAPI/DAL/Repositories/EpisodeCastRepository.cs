using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class EpisodeCastRepository : IRepository<EpisodeCast>
    {
        public EpisodeCastRepository(TVSeriesDbContext context)
        {
            _Context = context;
        }
        private TVSeriesDbContext _Context { get; init; }

        public async Task AddAsync(EpisodeCast obj)
        {
            await _Context.EpisodeCasts.AddAsync(obj);
        }

        public async Task DeleteAsync(EpisodeCast obj)
        {
            await Task.Run(() => _Context.EpisodeCasts.Remove(obj));
        }

        public async Task<ICollection<EpisodeCast>> GetAll()
        {
            return await _Context.EpisodeCasts.ToListAsync();
        }

        public async Task<EpisodeCast> GetById(int id)
        {
            return await _Context.EpisodeCasts.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _Context.SaveChangesAsync() != 0;
        }

        public async Task UpdateAsync(EpisodeCast obj)
        {
            await Task.Run(() => _Context.EpisodeCasts.Update(obj));
        }
    }
}
