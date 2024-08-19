using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Auth.Middlewares;

public class AuthCookieMiddleware : AuthMiddlewareUtils
{
    private readonly RequestDelegate _next;
    private const string SessionKey = "session";
    
    public AuthCookieMiddleware(RequestDelegate next) => _next = next;

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