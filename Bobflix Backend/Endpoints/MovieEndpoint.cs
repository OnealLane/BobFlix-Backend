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

           
            movieGroup.MapGet("{pageNum}", GetMoviesByPage);
            movieGroup.MapGet("{searchTerm}/{pageNum}", GetMoviesBySearch);
            movieGroup.MapGet("getBy/{id}", GetMovieById);

        }

      

        public static async Task<ApiResponseType<GetMoviesDto>> GetMoviesByPage(IMovieRepository movieRepository, int pageNum)
        {
            var result = await movieRepository.GetMoviesByPage(pageNum);
            if(result.CurrentPage > result.TotalPages)
            {
                return new ApiResponseType<GetMoviesDto>(false, "Failed to requeste movies by page", result);
            }

            return new ApiResponseType<GetMoviesDto>(true, "Successfully requested movies by page", result);
        }


        public static async Task<ApiResponseType<GetMoviesDto>> GetMoviesBySearch(IMovieRepository moviesRepository, string searchTerm, int pageNum)
        {
            var result = await moviesRepository.GetMoviesBySearch(searchTerm, pageNum);

            if(result.TotalPages < 1)
            {
                return new ApiResponseType<GetMoviesDto>(false, "Failed to request movies by search", result);
            }

            if (result.CurrentPage > result.TotalPages)
            {
                return new ApiResponseType<GetMoviesDto>(false, "Failed to requeste movies by page", result);
            }

            return new ApiResponseType<GetMoviesDto>(true, "Successfully requested movies by search", result);
        }

        public static async Task<ApiResponseType<GetMovieDto>> GetMovieById(IMovieRepository movieRepository, string id)
        {
            var result = await movieRepository.GetMovieById(id);

            if (result == null)
            {
                return new ApiResponseType<GetMovieDto>(false, "Failed to request movie by id", result);
            }

            return new ApiResponseType<GetMovieDto>(true, "Successfully requested movie by id", result);

        }
    }
}
