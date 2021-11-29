using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        public ICollection<Serie> Series { get; set; } = null!;
    }
}
