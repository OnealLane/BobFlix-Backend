using Bobflix_Backend.Helpers;
using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Bobflix_Backend.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DatabaseContext _db;
        private IUserHelper _userHelper;

        public MovieRepository(DatabaseContext db, IUserHelper userHelper)
        {
            _db = db;
            _userHelper = userHelper;
        }


        public async Task<GetMoviesDto> GetMoviesByPage(int pageNum)
        {
            int numMovies = _db.Movies.Count();
            double saus = numMovies / 10;
            int numPages = (int)Math.Ceiling(saus);

            ApplicationUser? currentUser = await _userHelper.GetCurrentUserAsync();
            UserMovie? userMovie = null;
            if (currentUser != null)
            {
                userMovie = await _db.UserMovies.FirstOrDefaultAsync(x => x.UserId == currentUser.Email);
            }

            var movies = await _db.Movies.Skip((pageNum - 1) * 10).Take(10).Select(x => new GetMovieDto()
            {
                ImdbId = x.ImdbId,
                Title = x.Title,
                Plot = x.Plot,
                PosterUrl = x.PosterUrl,
                Director = x.Director,
                Released = x.Released,
                AvgRating = x.AvgRating,
                CurrentUserRating = (userMovie == null) ? 0 : userMovie.Rating,
            }).ToListAsync();
            return new GetMoviesDto()
            {
                Movies = movies,
                CurrentPage = pageNum,
                TotalPages = numPages
            };
        }

        public async Task<GetMoviesDto> GetMoviesBySearch(string searchTerm, int pageNum)
        {
            var movies = await _db.Movies.ToListAsync();

            List<GetMovieDto> filteredMovies = new List<GetMovieDto>();
            ApplicationUser? currentUser = await _userHelper.GetCurrentUserAsync();
            UserMovie? userMovie = null;
            
            foreach (var movie in movies)
            {
                    if (currentUser != null)
                    {
                        userMovie = await _db.UserMovies.FirstOrDefaultAsync(x => x.ImdbId == movie.ImdbId && x.UserId == currentUser.Email);

                    }
                   

                var movieDto = new GetMovieDto()
                {
                    ImdbId = movie.ImdbId,
                    Title = movie.Title,
                    Plot = movie.Plot,
                    PosterUrl = movie.PosterUrl,
                    Director = movie.Director,
                    Released = movie.Released,
                    AvgRating = movie.AvgRating,
                    CurrentUserRating = (userMovie == null) ? 0 : userMovie.Rating,
                };

                if (movie.Title.ToLower().Contains(searchTerm.ToLower()))
                {
                    filteredMovies.Add(movieDto);
                }
            }

            var numMovies = filteredMovies.Count();
            double saus = numMovies / 10;
            int numPages = (int)Math.Ceiling(saus + 1);

            filteredMovies.Skip((pageNum - 1) * 10).Take(10);

            return new GetMoviesDto()
            {
                Movies = filteredMovies,
                CurrentPage = pageNum,
                TotalPages = numPages
            };
        }

        public async Task<GetMovieDto?> GetMovieById(string id)
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(x => x.ImdbId == id);
            ApplicationUser? currentUser = await _userHelper.GetCurrentUserAsync();
            UserMovie? userMovie = null;
            if (currentUser != null)
            {
                userMovie = await _db.UserMovies.FirstOrDefaultAsync(x => x.ImdbId == movie.ImdbId && x.UserId == currentUser.Email);

            }

            if (movie == null)
            {
                return null;
            }
            var movieDto = new GetMovieDto()
            {
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                Plot = movie.Plot,
                PosterUrl = movie.PosterUrl,
                Director = movie.Director,
                Released = movie.Released,
                AvgRating = movie.AvgRating,
                CurrentUserRating = (userMovie == null) ? 0 : userMovie.Rating,
            };
            return movieDto;
        }

    }
}
