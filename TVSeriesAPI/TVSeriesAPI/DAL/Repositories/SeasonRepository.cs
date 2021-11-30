using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class SeasonRepository : IRepository<Season>
    {
        public SeasonRepository(TVSeriesDbContext context)
        {
            _Context = context;
        }
        private TVSeriesDbContext _Context { get; init; }

        public async Task AddAsync(Season obj)
        {
            await _Context.Seasons.AddAsync(obj);
        }

        public async Task DeleteAsync(Season obj)
        {
            await Task.Run(() => _Context.Seasons.Remove(obj));
        }

        public async Task<ICollection<Season>> GetAll()
        {
            return await _Context.Seasons.ToListAsync();
        }

        public async Task<Season> GetById(int id)
        {
            return await _Context.Seasons.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _Context.SaveChangesAsync() != 0;
        }

        public async Task UpdateAsync(Season obj)
        {
            await Task.Run(() => _Context.Seasons.Update(obj));
        }
    }
}
