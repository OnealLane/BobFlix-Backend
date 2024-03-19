using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Bobflix_Backend.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();

        Task<GetMoviesDto> GetMoviesByPage(int pageNum);

        Task<IEnumerable<Movie>> GetMoviesBySearch(string searchTerm, int pageNum);
    }
}
