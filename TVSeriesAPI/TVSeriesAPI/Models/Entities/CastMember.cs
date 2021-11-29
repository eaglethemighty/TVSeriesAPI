namespace TVSeriesAPI.Models.Entities
{
    public class CastMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public ICollection<EpisodeCast> Episodes { get; set; }
    }
}
