using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Auth.Middlewares;

/// <summary>
/// Represents a middleware that handles authentication using cookies.
/// </summary>
public class AuthCookieMiddleware : AuthMiddlewareUtils
{
    /// <summary>
    /// Represents the next middleware in the pipeline.
    /// </summary>
    private readonly RequestDelegate _next;
    private const string SessionKey = "session";

    /// The AuthCookieMiddleware class is responsible for handling authentication using cookies.
    /// It checks if a session cookie is present in the incoming HTTP request. If the cookie is present,
    /// it extracts the token from the cookie and verifies it using the provided IAuthService implementation.
    /// If the token is valid, it processes the authenticated user by calling the ProcessAuthUser method
    /// from the AuthMiddlewareUtils base class. It then continues the request pipeline by calling the
    /// next middleware in the pipeline.
    /// If the session cookie is not present or the token is invalid, it skips the authentication process
    /// and continues the request pipeline by calling the next middleware in the pipeline.
    /// /
    public AuthCookieMiddleware(RequestDelegate next) => _next = next;

    /// <summary>
    /// Invokes the middleware asynchronously.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="authService">The authentication service.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public async Task InvokeAsync(
        HttpContext httpContext, 
        IAuthService authService,
        ILogger<AuthCookieMiddleware> logger)
    {
        
        httpContext.Request.Cookies.TryGetValue(SessionKey, out var sessionCookie);
        if (sessionCookie is not null)
        {
            if (authService.ExtractToken(sessionCookie, out var token))
            {
                var user = await authService.CheckToken(token!);
                ProcessAuthUser(user, httpContext, authService);
            }
        }

        await _next(httpContext);
    }
}