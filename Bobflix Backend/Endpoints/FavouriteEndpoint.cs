using Bobflix_Backend.ApiResponseType;
using Bobflix_Backend.Helpers;
using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;


namespace Bobflix_Backend.Endpoints
{
    public static class FavouriteEndpoint
    {
        public static void ConfigureFavouriteEndpoint(this WebApplication app)
        {
            var favouriteGroup = app.MapGroup("api/favourite/");

            favouriteGroup.MapPut("{ImdbId}", SetFavourite);
        }


        public static async Task<ApiResponseType<FavouriteDto?>> SetFavourite(IFavouriteRepository favouriteRepository, string ImdbId){

            var userMovie = await favouriteRepository.SetFavourite(ImdbId);

            if (userMovie == null)
            {
                return new ApiResponseType<FavouriteDto?>(false, "User was invalid.", null) ;
            }

            FavouriteDto favouriteDto = new FavouriteDto() { Favourite = userMovie.Favourite };

            return new ApiResponseType<FavouriteDto?>(true, "Successfully set favourite movie", favouriteDto);

        }


    }
}