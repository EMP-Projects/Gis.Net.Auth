using Gis.Net.Auth.Attributes;
using Gis.Net.Auth.DTO;
using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Auth.Middlewares
{
    public abstract class AuthMiddlewareUtils
    {
        protected static void ProcessAuthUser(AuthUserDto? user, HttpContext httpContext, IAuthService service)
        {
            if (user is null) return;
            httpContext.AddUser(user);
            service.LoggedUser = user;
        }
    }
}

