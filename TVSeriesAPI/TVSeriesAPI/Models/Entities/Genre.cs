namespace TVSeriesAPI.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Serie> Series { get; set; } = null!;
    }
}
