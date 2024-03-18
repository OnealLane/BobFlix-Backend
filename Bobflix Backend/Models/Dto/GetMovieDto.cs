namespace Bobflix_Backend.Models.Dto
{
    public class GetMovieDto
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Poster_url { get; set; }
        public string Director { get; set; }
        public string Released { get; set; }
        public int CurrentUserRating { get; set; }
        public int AverageRating { get; set; }

    }
}
