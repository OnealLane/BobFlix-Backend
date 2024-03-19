using Bobflix_Backend.Models;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bobflix_Backend.Repository
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private DatabaseContext _db;

        public FavouriteRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<UserMovie?> SetFavourite(string ImdbId)
        {
            var userMovie =  await _db.UserMovies.FirstOrDefaultAsync(x => x.ImdbId == ImdbId);
            if(userMovie == null)
            {
                UserMovie userMovie = new UserMovie() { ImdbId = ImdbId };
            }
            userMovie.Favourite = !userMovie.Favourite;
            await _db.SaveChangesAsync();
       
            return userMovie;
        }
    }
}
