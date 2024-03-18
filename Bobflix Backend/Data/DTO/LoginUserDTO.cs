using System.ComponentModel.DataAnnotations;

namespace Bobflix_Backend.Data.DTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
