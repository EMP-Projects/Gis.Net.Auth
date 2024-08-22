using Microsoft.AspNetCore.Builder;

namespace Gis.Net.Auth.Middlewares;

/// <summary>
/// Provides extension methods to configure the authentication cookie middleware.
/// </summary>
public static class AuthCookieExtension
{
    /// <summary>
    /// Adds the AuthCookie middleware to the request pipeline.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The same <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseCookie(this IApplicationBuilder builder) 
        => builder.UseMiddleware<AuthCookieMiddleware>();
}