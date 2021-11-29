using System.ComponentModel.DataAnnotations;
using TVSeriesAPI.Models.Enums;

namespace TVSeriesAPI.Models.Entities
{
    public class CastMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; } = null!;
        public CastPosition Position { get; set; }
        [Required]

        public ICollection<EpisodeCast> Episodes { get; set; } = null!;
    }
}
