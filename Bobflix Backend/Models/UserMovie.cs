using Bobflix_Backend.Endpoints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bobflix_Backend.Models
{
    [PrimaryKey(nameof(UserId), nameof(ImdbId))]
    [Table("UserMovie")]
    public class UserMovie
    {
        [ForeignKey("ApplicationUser")]
        [Column("userId")]
        public string UserId { get; set; }
        [ForeignKey("Movie")]
        [Column("imdbId")]
        public int ImdbId { get; set; }
        [Column("favourite")]
        public bool Favourite {  get; set; }
        [Column("rating")]
        public int Rating { get; set; }
    }
}
