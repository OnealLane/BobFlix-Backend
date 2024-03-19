using Bobflix_Backend.Models;

namespace Bobflix_Backend.Repository.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<UserMovie> SetFavourite(string ImdbId);
    }
}
