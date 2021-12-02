using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.DTOs
{
    public class SerieQuery
    {
        [Range(1, 99999)]
        public int Page { get; set; } = 1;

        [StringLength(255, MinimumLength = 1)]
        public string Filter { get; set; } = "*";

        public bool Sort { get; set; } = false;
    }
}
