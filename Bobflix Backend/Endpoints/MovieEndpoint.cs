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
            var movieGroup = app.MapGroup("api/movies");

            movieGroup.MapGet("", GetMovies);
            movieGroup.MapGet("{pageNum}", GetMoviesByPage);
            movieGroup.MapGet("{searchTerm}/{pageNum}", GetMoviesBySearch);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<ApiResponseType<List<Movie>>> GetMovies(IMovieRepository movieRepository)
        {
             var result = await movieRepository.GetMovies();

            return new ApiResponseType<List<Movie>>(true, "Successfully requested all movies", result);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<ApiResponseType<GetMoviesDto>> GetMoviesByPage(IMovieRepository movieRepository, int pageNum)
        {
            var result = await movieRepository.GetMoviesByPage(pageNum);

            return new ApiResponseType<GetMoviesDto>(true, "Successfully requested movies by page", result);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<ActionResult<ApiResponseType<IEnumerable<Movie>>>> GetMoviesBySearch(IMovieRepository moviesRepository, string searchTerm, int pageNum)
        {
            var result = await moviesRepository.GetMoviesBySearch(searchTerm, pageNum);
            return new ApiResponseType<IEnumerable<Movie>>(true, "Successfully requested movies by page", result);
        }
    }
}
