using System.Security.Claims;

namespace HumanResources.GlobalMethods
{
    public static class AuthMethods
    {
        public static string GetCurrentUserId(ClaimsPrincipal User)
        {
            var currentUser = User.Identity as ClaimsIdentity;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return currentUserId;
        }
    }
}
