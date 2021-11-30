using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class GenreReadDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; } = null!;
    }
}
