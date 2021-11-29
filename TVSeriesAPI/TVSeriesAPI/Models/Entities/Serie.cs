namespace TVSeriesAPI.Models.Entities
{
    public class Serie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public IList<Season> Seasons { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
