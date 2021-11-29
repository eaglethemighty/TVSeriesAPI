namespace TVSeriesAPI.Models.Entities
{
    public class EpisodeCast
    {
        public int Id { get; set; }
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; } = null!;
        public int CastMemberId { get; set; }
        public CastMember CastMember { get; set; } = null!;
    }
}
