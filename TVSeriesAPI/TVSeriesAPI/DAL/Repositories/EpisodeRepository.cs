using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode>
    {
        public EpisodeRepository(TVSeriesDbContext dataContext) : base(dataContext)
        {
        }
    }
}
