using Bobflix_Backend.ApiResponseType;
using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Bobflix_Backend.Endpoints
{
    public static class MovieEndpoint
    {
        public static void ConfigureMovieEndpoint(this WebApplication app)
        {
            var movieGroup = app.MapGroup("movies");

            movieGroup.MapGet("", GetMovies);
            movieGroup.MapGet("{pageNum}", GetMoviesByPage);
            movieGroup.MapGet("{searchTerm}/{pageNum}", GetMoviesBySearch);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<ApiResponseType<IEnumerable<Movie>>> GetMovies(IMovieRepository movieRepository)
        {
             var result = await movieRepository.GetMovies();

            return new ApiResponseType<IEnumerable<Movie>>(true, "Successfully requested all movies", result);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<ApiResponseType<List<GetMovieDto>>> GetMoviesByPage(IMovieRepository movieRepository, int pageNum)
        {
            var moviesByPage = await movieRepository.GetMoviesByPage(pageNum);

            List<GetMovieDto> movies = new List<GetMovieDto>();

            foreach (var entity in moviesByPage)
            {
                var movie = new GetMovieDto()
                {
                    ImdbId = entity.ImdbId,
                    Title = entity.Title,
                    Plot = entity.Plot,
                    Poster_url = entity.Poster_url,
                    Director = entity.Director,
                    Released = entity.Released,
                    AverageRating = 1,
                    CurrentUserRating = 1,
                };
                movies.Add(movie);  
            }

            return new ApiResponseType<List<GetMovieDto>>(true, "Successfully requested movies by page", movies);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<ActionResult<ApiResponseType<IEnumerable<Movie>>>> GetMoviesBySearch(IMovieRepository moviesRepository, string searchTerm, int pageNum)
        {
            var result = await moviesRepository.GetMoviesBySearch(searchTerm, pageNum);
            return new ApiResponseType<IEnumerable<Movie>>(true, "Successfully requested movies by page", result);
        }
    }
}
