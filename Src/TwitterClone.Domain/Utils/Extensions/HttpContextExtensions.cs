using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TwitterClone.Domain.Constants;

namespace TwitterClone.Domain.Utils.Extensions;
public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext httpContext)
    {
        var user = httpContext.User;

        var identity = (ClaimsIdentity?)user.Identity;

        var claims = identity?.Claims;

        var userIdClaim = claims?.First(x => x.Type == ClaimsConstants.UserId);

        return Guid.Parse(userIdClaim?.Value ?? "");
    }
}