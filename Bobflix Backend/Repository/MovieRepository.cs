using Bobflix_Backend.Models;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bobflix_Backend.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DatabaseContext _db;

        public MovieRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _db.Movies.ToListAsync();
        }
    }
}
