using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public GenreRepository(TVSeriesDbContext context)
        {
            _Context = context;
        }
        private TVSeriesDbContext _Context { get; init; }

        public async Task AddAsync(Genre obj)
        {
            await _Context.Genres.AddAsync(obj);
        }

        public async Task DeleteAsync(Genre obj)
        {
            await Task.Run(() => _Context.Genres.Remove(obj));
        }

        public async Task<ICollection<Genre>> GetAll()
        {
            return await _Context.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return await _Context.Genres.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _Context.SaveChangesAsync() != 0;
        }

        public async Task UpdateAsync(Genre obj)
        {
            await Task.Run(() => _Context.Genres.Update(obj));
        }
    }
}
