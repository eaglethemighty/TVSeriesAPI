using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class SeasonRepository : BaseRepository<Season>
    {
        public SeasonRepository(TVSeriesDbContext dataContext) : base(dataContext)
        {
        }
    }
}
