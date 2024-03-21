using Bobflix_Backend.Models.Dto;

namespace Bobflix_Backend.Data.DTO
{
    public class GetUserWInfoDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public double AvgRating { get; set; } = 0;
        public List<GetMovieDto> favouriteMovies { get; set; } = [];

        public string? imgUrl { get; set; }
    }
}
