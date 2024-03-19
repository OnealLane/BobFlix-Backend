using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Bobflix_Backend.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();

        Task<GetMoviesDto> GetMoviesByPage(int pageNum);

        Task<GetMoviesDto> GetMoviesBySearch(string searchTerm, int pageNum);

        Task<GetMovieDto> GetMovieById(string id);
    }
}
