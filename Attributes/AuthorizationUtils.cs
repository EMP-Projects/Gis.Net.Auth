using Gis.Net.Auth.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Auth.Attributes;

public static class AuthorizationUtils
{
        
    public static AuthUserDto? RetrieveUser(this HttpContext context)
    {
        return (AuthUserDto?)context.Items["User"];
    }

    public static void AddUser(this HttpContext context, AuthUserDto authUser) => context.Items.Add("User", authUser);

    public static IActionResult Unauthorized() => new UnauthorizedResult();
}