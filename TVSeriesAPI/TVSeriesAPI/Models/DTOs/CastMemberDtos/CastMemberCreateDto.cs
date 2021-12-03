using System.ComponentModel.DataAnnotations;
using TVSeriesAPI.Models.DTOs.CastMemberDtos;
using TVSeriesAPI.Models.Enums;

namespace TVSeriesAPI.Models.DTOs
{
    public class CastMemberCreateDto
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Required]
        [ExistInEnum]
        public CastPosition Position { get; set; }
    }
}
