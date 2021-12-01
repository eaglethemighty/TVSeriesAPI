using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(TVSeriesDbContext dataContext) : base(dataContext)
        {
        }
    }
}
