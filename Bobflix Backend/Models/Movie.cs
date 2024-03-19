using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Bobflix_Backend.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("imdbId")]
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Released { get; set; }
        public string PosterUrl { get; set; }
        public string Plot {  get; set; }
        public double AvgRating { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}
