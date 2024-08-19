using Gis.Net.Auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Auth.Middlewares
{
    /// <summary>
    /// Middleware that processes the HTTP context to authenticate a user based on an API key.
    /// </summary>
    public class AuthByKeyMiddleware : AuthMiddlewareUtils
    {
        private readonly RequestDelegate _next;

        public AuthByKeyMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Middleware invocation method that processes the HTTP context to authenticate a user based on an API key.
        /// </summary>
        /// <param name="httpContext">The current HTTP context representing the HTTP request and response.</param>
        /// <param name="auth">The authentication service used to validate the API key.</param>
        /// <returns>A task representing the asynchronous operation of the middleware invocation and authentication process.</returns>
        /// <remarks>
        /// This method retrieves the API key from the request header using the key name provided by the 'auth' service.
        /// If an API key is present, it calls the 'CheckApiKey' method of the 'auth' service to validate the key and process the user.
        /// Regardless of the outcome, it ensures that the request is passed along the middleware pipeline by calling '_next'.
        /// </remarks>
        public async Task InvokeAsync(HttpContext httpContext, IAuthService auth, ILogger<AuthByKeyMiddleware> logger)
        {
            var authorizationHeaderValue = httpContext.Request.Headers[auth.AuthorizationHeaderApiKey].FirstOrDefault();
            if (authorizationHeaderValue is not null)
            {
                try
                {
                    var user = await auth.CheckApiKey(authorizationHeaderValue);
                    ProcessAuthUser(user, httpContext, auth);
                }
                catch (Exception e)
                {
                    logger.LogError($"{nameof(auth.CheckApiKey)} failed => {e.Message}");
                }
            }
            await _next(httpContext);
        }
    }

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class ApiKeyAuthExtension
    {
        /// <summary>
        /// Aggiunge il middleware che controlla l'header ApiKey per autorizzare una richiesta
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiKeyAuth(this IApplicationBuilder builder) 
            => builder.UseMiddleware<AuthByKeyMiddleware>();
    }
}



