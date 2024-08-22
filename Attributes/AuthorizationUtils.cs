using Gis.Net.Auth.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Auth.Attributes;

/// <summary>
/// Utility class for authorization related operations.
/// </summary>
public static class AuthorizationUtils
{

    /// <summary>
    /// Retrieves the authenticated user from the HttpContext.
    /// </summary>
    /// <param name="context">The HttpContext object.</param>
    /// <returns>The AuthUserDto object representing the authenticated user. Returns null if no user is authenticated.</returns>
    public static AuthUserDto? RetrieveUser(this HttpContext context)
    {
        return (AuthUserDto?)context.Items["User"];
    }

    /// <summary>
    /// Adds the authenticated user to the HttpContext.
    /// </summary>
    /// <param name="context">The HttpContext.</param>
    /// <param name="authUser">The AuthUserDto object representing the authenticated user.</param>
    public static void AddUser(this HttpContext context, AuthUserDto authUser) => context.Items.Add("User", authUser);

    /// <summary>
    /// Returns an Unauthorized result.
    /// </summary>
    /// <returns>An UnauthorizedResult object.</returns>
    public static IActionResult Unauthorized() => new UnauthorizedResult();
}