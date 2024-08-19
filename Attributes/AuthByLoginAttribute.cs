using Gis.Net.Auth.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gis.Net.Auth.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthByLoginAttribute : Attribute, IAuthorizationFilter
{
    protected AuthUserDto? AuthUser { get; private set; }

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