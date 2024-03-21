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
        private IUserHelper _userHelper;

        public FavouriteRepository(DatabaseContext db, UserManager<ApplicationUser> userManager, IUserHelper userHelper)
        {
            _db = db;
            _userManager = userManager;
            _userHelper = userHelper;
        }


        public async Task<UserMovie?> SetFavourite(string ImdbId)
        {

            var currentUser = await _userHelper.GetCurrentUserAsync();
            if (currentUser == null)
            {
                return null;
            }
            var userMovie =  await _db.UserMovies.FirstOrDefaultAsync(x => x.ImdbId == ImdbId && x.UserId == currentUser.Email);

            if(userMovie == null)
            {
                UserMovie newUserMovie = new UserMovie() { ImdbId = ImdbId, UserId = currentUser.Email, Favourite = true, UsersId = currentUser.Id };
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
