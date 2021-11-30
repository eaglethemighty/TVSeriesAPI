using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class EpisodeRepository : IRepository<Episode>
    {
        public EpisodeRepository(TVSeriesDbContext context)
        {
            _Context = context;
        }
        private TVSeriesDbContext _Context { get; init; }

        public async Task AddAsync(Episode obj)
        {
            await _Context.Episodes.AddAsync(obj);
        }

        public async Task DeleteAsync(Episode obj)
        {
            await Task.Run(() => _Context.Episodes.Remove(obj));
        }

        public async Task<ICollection<Episode>> GetAll()
        {
            return await _Context.Episodes.ToListAsync();
        }

        public async Task<Episode> GetById(int id)
        {
            return await _Context.Episodes.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _Context.SaveChangesAsync() != 0;
        }

        public async Task UpdateAsync(Episode obj)
        {
            await Task.Run(() => _Context.Episodes.Update(obj));
        }
    }
}
