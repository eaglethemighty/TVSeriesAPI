namespace TVSeriesAPI.Models.Entities
{
    public class TVSerie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public IList<Season> Seasons { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
