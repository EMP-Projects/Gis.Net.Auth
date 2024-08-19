using Microsoft.AspNetCore.Builder;

namespace Gis.Net.Auth.Middlewares;

public static class AuthCookieExtension
{
    public static IApplicationBuilder UseCookie(this IApplicationBuilder builder) 
        => builder.UseMiddleware<AuthCookieMiddleware>();
}