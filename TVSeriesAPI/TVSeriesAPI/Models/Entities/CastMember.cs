using TVSeriesAPI.Models.Enums;

namespace TVSeriesAPI.Models.Entities
{
    public class CastMember
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CastPosition Position { get; set; }
        public ICollection<EpisodeCast> Episodes { get; set; } = null!;
    }
}
