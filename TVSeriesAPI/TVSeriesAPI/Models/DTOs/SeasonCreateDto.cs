using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class SeasonCreateDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        public int SerieId { get; set; }
    }
}
