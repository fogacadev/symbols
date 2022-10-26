using System.Security.Claims;

namespace Symbols.Api.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string IdentifierName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if(claim == null)
                return "";

            return claim.Value;
        }
    }
}
