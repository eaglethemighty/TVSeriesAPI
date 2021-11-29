using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Entities
{
    public class EpisodeCast
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; } = null!;

        [Required]
        public int CastMemberId { get; set; }
        public CastMember CastMember { get; set; } = null!;
    }
}
