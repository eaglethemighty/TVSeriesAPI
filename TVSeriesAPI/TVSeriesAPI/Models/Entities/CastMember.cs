using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Entities
{
    public class CastMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Required]
        public int Position { get; set; }

        public ICollection<EpisodeCast> Episodes { get; set; } = null!;
    }
}
