using System.Security.Claims;

namespace Bobflix_Backend.Helpers
{
    public static class ClaimsPrincipalHelper
    {
        public static string? Email(this ClaimsPrincipal user)
        {
            Claim? claim = user.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
