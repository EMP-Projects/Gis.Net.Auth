using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Auth.Middlewares
{
    public class AuthByLoginMiddleware : AuthMiddlewareUtils
    {
        private readonly RequestDelegate _next;

        public AuthByLoginMiddleware(RequestDelegate next) => _next = next;

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

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthExtensions
    {
        public static IApplicationBuilder UseLoginAuth(this IApplicationBuilder builder)
            => builder.UseMiddleware<AuthByLoginMiddleware>();
    }
}