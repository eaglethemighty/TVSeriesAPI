using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class EpisodeCastRepository : BaseRepository<EpisodeCast>
    {
        public EpisodeCastRepository(TVSeriesDbContext dataContext) 
            :base(dataContext)
        {
        }
    }
}
