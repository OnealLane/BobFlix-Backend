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

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetMovies(IMovieRepository movieRepository)
        {
             var result = await movieRepository.GetMovies();

            return TypedResults.Ok(result);

        }
    }
}
