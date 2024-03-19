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

        public async Task<List<Movie>> GetMovies()
        {
            return await _db.Movies.ToListAsync();
        }

        public async Task<GetMoviesDto> GetMoviesByPage(int pageNum)
        {
            int numMovies = _db.Movies.Count();
            double saus = numMovies / 10;
            int numPages = (int)Math.Ceiling(saus);
            var movies = await _db.Movies.Skip((pageNum - 1) * 10).Take(10).Select(x => new GetMovieDto()
            {
                ImdbId = x.ImdbId,
                Title = x.Title,
                Plot = x.Plot,
                PosterUrl = x.PosterUrl,
                Director = x.Director,
                Released = x.Released,
                AvgRating = x.AvgRating,
                CurrentUserRating = 1,



            }).ToListAsync();
            return new GetMoviesDto()
            {
                Movies = movies,
                CurrentPage = pageNum,
                TotalPages = numPages
            };
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
