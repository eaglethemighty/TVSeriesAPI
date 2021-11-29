namespace TVSeriesAPI.Models.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        public ICollection<EpisodeCast> CastMembers { get; set; }

    }
}
