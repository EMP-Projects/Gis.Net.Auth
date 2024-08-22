using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Auth.Middlewares;

/// <summary>
/// Represents a middleware for authentication by login.
/// </summary>
public class AuthByLoginMiddleware : AuthMiddlewareUtils
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Represents a middleware for authentication by login.
    /// </summary>
    public AuthByLoginMiddleware(RequestDelegate next) => _next = next;

    /// <summary>
    /// Invokes the authentication middleware for authentication by login.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
    /// <param name="service">The <see cref="IAuthService"/> service.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext httpContext, IAuthService service)
    {
        var authorizationHeaderValue =
            httpContext.Request.Headers[service.AuthorizationHeaderLogin].FirstOrDefault();
        if (authorizationHeaderValue != null)
        {
            if (service.ExtractToken(authorizationHeaderValue, out var token))
            {
                var user = await service.CheckToken(token!);
                ProcessAuthUser(user, httpContext, service);
            }
        }

        await _next(httpContext);
    }
}

/// <summary>
/// Provides extension methods for configuring authentication middleware.
/// </summary>
public static class AuthExtensions
{
    public static IApplicationBuilder UseLoginAuth(this IApplicationBuilder builder)
        => builder.UseMiddleware<AuthByLoginMiddleware>();
}