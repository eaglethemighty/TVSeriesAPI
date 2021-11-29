namespace TVSeriesAPI.Models.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int SerieId { get; set; }
        public Serie Serie { get; set; } = null!;
        public IList<Episode> Episodes { get; set; } = null!;
    }
}
