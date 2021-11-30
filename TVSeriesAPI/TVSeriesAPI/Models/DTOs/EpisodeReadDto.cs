namespace TVSeriesAPI.Models.DTOs
{
    public class EpisodeReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Number { get; set; }
        public int SeasonId { get; set; }
    }
}
