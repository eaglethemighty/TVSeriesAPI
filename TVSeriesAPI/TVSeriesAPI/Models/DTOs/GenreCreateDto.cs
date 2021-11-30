using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class GenreCreateDto
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; } = null!;
    }
}
