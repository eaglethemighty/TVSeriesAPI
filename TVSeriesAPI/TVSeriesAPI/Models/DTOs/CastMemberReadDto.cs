using TVSeriesAPI.Models.Enums;

namespace TVSeriesAPI.Models.DTOs
{
    public class CastMemberReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CastPosition Position { get; set; }
    }
}
