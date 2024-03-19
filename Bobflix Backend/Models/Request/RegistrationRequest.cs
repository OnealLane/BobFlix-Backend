using Bobflix_Backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bobflix_Backend.Models
{
    public class RegistrationRequest
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public Role role { get; set; } = Role.User;

    }
}
