using Bobflix_Backend.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMDbApiNet;
using OMDbApiNet.Model;

namespace Bobflix_Backend
{
    public class DatabaseContext : IdentityUserContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Movie> Movies = new List<Movie>();
            OmdbClient omdb = new OmdbClient("4f687ad8");
            for( int i = 1; i<=10; i++)
            {
                SearchList sL = omdb.GetSearchList("bob", i);
                foreach (var searchItem in sL.SearchResults)
                {
                    Item item = omdb.GetItemById(searchItem.ImdbId, true);
                    Movie movie = new Movie { ImdbId = item.ImdbId, Title = item.Title, Director = item.Director, Released = item.Released, 
                        PosterUrl = item.Poster, Plot = item.Plot, AvgRating = Math.Round(double.Parse(item.ImdbRating), 2)};
                    Movies.Add(movie);
                }

            }
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasMany(m => m.Users).WithMany(u => u.Movies).UsingEntity<UserMovie>();
            modelBuilder.Entity<Movie>().HasData(Movies);
        }

        public DbSet<Movie> Movies { get; set; }
        
    }
}
