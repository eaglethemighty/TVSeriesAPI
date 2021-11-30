using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class SerieRepository : ISerieRepository
    {
        public SerieRepository(TVSeriesDbContext context)
        {
            _Context = context;
        }
        private TVSeriesDbContext _Context { get; init; }

        public async Task AddAsync(Serie obj)
        {
            await _Context.Series.AddAsync(obj);
        }

        public async Task DeleteAsync(Serie obj)
        {
            await Task.Run(() => _Context.Series.Remove(obj));
        }

        public async Task<ICollection<Serie>> GetAll()
        {
            return await _Context.Series.ToListAsync();
        }

        public async Task<Serie> GetById(int id)
        {
            return await _Context.Series.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _Context.SaveChangesAsync() != 0;
        }

        public async Task UpdateAsync(Serie obj)
        {
            await Task.Run(() => _Context.Series.Update(obj));
        }
    }
}
