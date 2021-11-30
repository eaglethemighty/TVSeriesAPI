namespace TVSeriesAPI.Models.DTOs
{
    public class SerieReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }
    }
}
