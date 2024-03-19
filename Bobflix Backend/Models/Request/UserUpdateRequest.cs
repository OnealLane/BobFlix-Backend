using System.ComponentModel.DataAnnotations;

namespace Bobflix_Backend.Models.Request
{
    public class UserUpdateRequest
    {

        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string NewPassword { get; set; }

    }
}
