using Bobflix_Backend.Models;

namespace Bobflix_Backend.Repository.Interfaces
{
    public interface IRateRepository
    {

        public  Task<UserMovie> UpdateRating(string ImdbId, int rating);
    }
}
