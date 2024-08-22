using Gis.Net.Auth.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gis.Net.Auth.Attributes;

/// <summary>
/// Attribute that is used to authorize a user based on their login credentials.
/// </summary>
/// <remarks>
/// A user must provide valid login credentials in order to access methods and endpoints decorated with this attribute.
/// If the user is not authenticated, a 401 Unauthorized response will be returned.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthByLoginAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Represents an authenticated user.
    /// </summary>
    protected AuthUserDto? AuthUser { get; private set; }

    /// <summary>
    /// Represents an attribute that allows authorization based on login.
    /// </summary>
    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
        AuthUser = context.HttpContext.RetrieveUser();
        if (AuthUser is not null)
            return;
        
        var errorDto = new ErrorResponse("Authorization failed");
        var jsonResult = new JsonResult(errorDto)
        {
            StatusCode = StatusCodes.Status401Unauthorized 
        };

        context.Result = jsonResult;
    }
}