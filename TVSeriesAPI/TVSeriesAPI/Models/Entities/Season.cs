using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Entities
{
    public class Season
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        public int SerieId { get; set; }
        public Serie Serie { get; set; } = null!;

        public IList<Episode> Episodes { get; set; } = null!;
    }
}
