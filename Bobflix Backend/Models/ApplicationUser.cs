using Bobflix_Backend.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bobflix_Backend.Models
{
    public class ApplicationUser : IdentityUser
    {

      
        public Role Role { get; set; }
    }
}
