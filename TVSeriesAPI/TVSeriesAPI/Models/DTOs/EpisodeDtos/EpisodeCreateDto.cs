using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class EpisodeCreateDto
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        public int SeasonId { get; set; }
    }
}
