using Bobflix_Backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Bobflix_Backend.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies();

        Task<IEnumerable<Movie>> GetMoviesByPage(int pageNum);

        Task<IEnumerable<Movie>> GetMoviesBySearch(string searchTerm, int pageNum);
    }
}
