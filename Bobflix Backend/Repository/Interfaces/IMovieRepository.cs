using Bobflix_Backend.Models;

namespace Bobflix_Backend.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies();
    }
}
