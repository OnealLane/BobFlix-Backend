using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        public async Task<List<Movie>> GetMoviesByPage(int pageNum)
        {
            int numMovies = _db.Movies.Count();
            double saus = numMovies / 10;
            int numPages = (int)Math.Ceiling(saus);
            var movies = await _db.Movies.Skip((pageNum - 1) * 10).Take(10).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesBySearch(string searchTerm, int pageNum)
        {
            var movies = await _db.Movies.ToListAsync();
            List<Movie> filteredMovies = new List<Movie>();

            foreach(var movie in movies)
            {
                if (movie.Title.Contains(searchTerm))
                {
                    filteredMovies.Add(movie);
                }
            }

            return filteredMovies.Skip((pageNum - 1) * 10).Take(10);    

            
            
        }
               
    }
}
