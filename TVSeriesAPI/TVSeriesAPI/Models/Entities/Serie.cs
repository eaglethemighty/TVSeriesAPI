using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Entities
{
    public class Serie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        public IList<Season> Seasons { get; set; } = null!;

        [Required]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
