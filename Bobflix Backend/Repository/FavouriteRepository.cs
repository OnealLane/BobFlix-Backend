using Bobflix_Backend.Helpers;
using Bobflix_Backend.Models;
using Bobflix_Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bobflix_Backend.Repository
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private DatabaseContext _db;
        private UserManager<ApplicationUser> _userManager;

        public FavouriteRepository(DatabaseContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public async Task<UserMovie> SetFavourite(string ImdbId)
        {

            var movie = await _db.Movies.FirstOrDefaultAsync(x => x.ImdbId == ImdbId);
            var userMovie =  await _db.UserMovies.FirstOrDefaultAsync(x => x.ImdbId == ImdbId);

            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            IUserHelper userHelper = new UserHelper(httpContextAccessor, _db);

            var currentUser = await userHelper.GetCurrentUserAsync();

            if(userMovie == null)
            {
                UserMovie newUserMovie = new UserMovie() { ImdbId = movie.ImdbId, UserId = currentUser.Email, Favourite = true, UsersId = currentUser.Id };
                await _db.UserMovies.AddAsync(newUserMovie);
                await _db.SaveChangesAsync();
                return newUserMovie;
            }

            userMovie.Favourite = !userMovie.Favourite;
            await _db.SaveChangesAsync();
       
            return userMovie;
        }
    }
}
