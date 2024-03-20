using Bobflix_Backend.Helpers;
using Bobflix_Backend.Models;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bobflix_Backend.Repository
{
    public class RateRepository : IRateRepository
    {
        private IUserHelper _userHelper;
        private DatabaseContext _db;

        public RateRepository(DatabaseContext db, IUserHelper userHelper)
        {
            _db = db;
            _userHelper = userHelper;
        }

        public async Task<UserMovie> UpdateRating( string ImdbId, int rating)
        {
            var user = await _userHelper.GetCurrentUserAsync();
            var movie = await _db.UserMovies.FindAsync(user.Email, ImdbId);
            //  var userMovie = await _db.UserMovies.FirstOrDefaultAsync(x => x.ImdbId == ImdbId);
            if (movie == null)
            {
                UserMovie newUserMovie = new UserMovie() { ImdbId = movie.ImdbId, UserId = user.Email, UsersId = user.Id, Rating = rating };
                await _db.UserMovies.AddAsync(newUserMovie);
                await _db.SaveChangesAsync();
                return newUserMovie;
            }

            movie.Rating = rating;
            await _db.SaveChangesAsync();
            return movie;
            

        }
          
    }
}
