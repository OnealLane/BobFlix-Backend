namespace Bobflix_Backend.Models.Dto
{
    public class GetMoviesDto
    {
        public List<GetMovieDto> Movies { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
