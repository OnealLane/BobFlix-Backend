using Bobflix_Backend.ApiResponseType;
using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Dto;
using Bobflix_Backend.Repository;
using Bobflix_Backend.Repository.Interfaces;

namespace Bobflix_Backend.Endpoints
{
    public static class RateEndpoint
    {


        public static void ConfigureRateEndpoint(this WebApplication app)
        {
            var rateGroup = app.MapGroup("api/rate/");
            rateGroup.MapPut("/{imdbId}/{rating}", UpdateRating);


        }



        public static async Task<IResult> UpdateRating(IRateRepository repository, string imdbId, int rating)
        {
            var userMovie = await repository.UpdateRating(imdbId, rating);
            RateDto rateDto = new RateDto() { Rating = userMovie.Rating };
            return TypedResults.Ok(new ApiResponseType<RateDto>(true, "New rating is: ", rateDto));
        }
    }
}
