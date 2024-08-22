using Gis.Net.Auth.Attributes;
using Gis.Net.Auth.DTO;
using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Auth.Middlewares;

/// <summary>
/// A utility class used by authentication middlewares to process authentication user information.
/// </summary>
public abstract class AuthMiddlewareUtils
{
    /// <summary>
    /// Processes the authenticated user information.
    /// </summary>
    /// <param name="user">The authenticated user information.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="service">The authentication service</param>
    protected static void ProcessAuthUser(AuthUserDto? user, HttpContext httpContext, IAuthService service)
    {
        if (user is null) return;
        httpContext.AddUser(user);
        service.LoggedUser = user;
    }
}