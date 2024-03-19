using Microsoft.EntityFrameworkCore;

namespace Bobflix_Backend.Repository
{
    public class RateRepository
    {

        private DatabaseContext _db;

        public RateRepository(DatabaseContext db)
        {
            _db = db;
        }

        /*  public async Task<bool> updateRating(string userId, string ImdbId, int rating)

           {
               var movie = await _db.Movies.FirstOrDefaultAsync(m => m.ImdbId == ImdbId);
               if (movie == null)
               {
                   return false;
               }


           }
          */
    }
}
