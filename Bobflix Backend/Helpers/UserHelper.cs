using Bobflix_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Bobflix_Backend.Helpers
{
    public interface IUserHelper
    {
        Task<ApplicationUser> GetCurrentUserAsync();
    }
    public class UserHelper : IUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContext _context;
        public UserHelper(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            string email = _httpContextAccessor.HttpContext.User.Email();
            ApplicationUser user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
