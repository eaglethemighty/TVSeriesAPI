using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class SerieCreateDto
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}
