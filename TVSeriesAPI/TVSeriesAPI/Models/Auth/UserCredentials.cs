using System.ComponentModel.DataAnnotations;

namespace TVSeriesAPI.Models.Auth
{
    public class UserCredential
    {
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Password { get; set; }
    }
}
