using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository(TVSeriesDbContext dataContext) : base(dataContext)
        {
        }
    }
}
