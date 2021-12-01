using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class CastMemberRepository : BaseRepository<CastMember>
    {
        public CastMemberRepository(TVSeriesDbContext context)
            :base(context)
        {
        }
    }
}
