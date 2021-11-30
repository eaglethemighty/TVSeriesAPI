using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Entities
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; } = null!;
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        public int SeasonId { get; set; }

        public Season Season { get; set; } = null!;

        public ICollection<EpisodeCast> CastMembers { get; set; } = null!;

    }
}
