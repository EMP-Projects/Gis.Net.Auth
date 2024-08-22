using System.Security.Authentication;
using AutoMapper;
using Gis.Net.Auth.Attributes;
using Gis.Net.Auth.DTO;
using Gis.Net.Auth.Services;
using Gis.Net.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Auth.Controllers;

/// <inheritdoc />
public abstract class AuthController : RootControllerBase
{
    protected readonly IAuthService AuthService;

    /// <inheritdoc />
    protected AuthController(ILogger<AuthController> logger,
        IConfiguration configuration,
        IAuthService auth,
        IMapper mapper) : base(logger, configuration, mapper)
    {
        AuthService = auth;
    }

    /// <inheritdoc />
    /// <summary>
    /// Signs in a user with the provided login credentials.
    /// </summary>
    /// <param name="loginRequestDto">The login credentials of the user.</param>
    /// <returns>An IActionResult representing the sign-in result.</returns>
    [HttpPost("signIn")]
    public virtual async Task<IActionResult> SignIn([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
            var loginDto = Mapper.Map<LoginDto>(loginRequestDto);
            var authUser = await AuthService.SignIn(loginDto);
            var jwtToken = await AuthService.GetUserToken(authUser);
            AuthService.LoggedUser = authUser;
            return Ok(new AuthResponseDto(jwtToken));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }

    /// <summary>
    /// Gets the currently logged-in user.
    /// </summary>
    /// <remarks>
    /// Returns the <see cref="AuthUserDto"/> object representing the currently logged-in user.
    /// If there is no logged-in user, a 401 Unauthorized response will be returned.
    /// </remarks>
    /// <returns>The currently logged-in user as an <see cref="IActionResult"/>.</returns>
    [HttpGet("me"), AuthByLogin]
    public virtual async Task<IActionResult> GetLoggedUser()
    {
        try
        {
            var result = await Task.Run(() => AuthService.LoggedUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }

    /// <summary>
    /// Signs up a user with the provided details.
    /// </summary>
    /// <param name="signUpRequestDto">The details of the user to sign up.</param>
    /// <returns>An IActionResult representing the sign-up result.</returns>
    [HttpPost("signup")]
    public virtual async Task<IActionResult> SignUp([FromBody] ReducedUserRequestDto signUpRequestDto)
    {
        try
        {
            var signUpDto = Mapper.Map<ReducedUserDto>(signUpRequestDto);
            var authUser = await AuthService.SignUp(signUpDto);

            if (authUser is null)
                return SingleResultWithError<AuthUserDto>("User registration failed.");

            AuthService.LoggedUser = authUser;
            return Ok(new AuthResponseDto(authUser.Token!));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }

    /// <summary>
    /// Sets the login credentials for a user.
    /// </summary>
    /// <param name="loginDto">The login credentials to be set.</param>
    /// <returns>An IActionResult representing the result of setting the credentials.</returns>
    [HttpPost("credentials/set")]
    public virtual async Task<IActionResult> Credentials([FromBody] LoginDto loginDto)
    {
        try
        {
            await AuthService.SetCredentials(loginDto);
            return Ok(new AuthResponseDto(loginDto.Username!));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }

    /// <summary>
    /// Changes the password for the logged-in user.
    /// </summary>
    /// <param name="changePwdDto">The <see cref="ChangePasswordDto"/> object containing the old and new passwords.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the password change operation.</returns>
    [HttpPost("change/password"), AuthByLogin]
    public virtual async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePwdDto)
    {
        try
        {
            if (string.IsNullOrEmpty(changePwdDto.OldPassword) || string.IsNullOrEmpty(changePwdDto.NewPassword))
                return SingleResultWithError<AuthUserDto>("Passwords must be specified");

            await AuthService.ChangePassword(changePwdDto);
            Logger.LogInformation($"Password for user {AuthService.LoggedUser!.Username} updated successfully");
            await AuthService.SaveContext();
            return NoContent();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }

    /// <summary>
    /// Changes the API key for the logged-in user.
    /// </summary>
    /// <param name="changeKeyDto">The data transfer object containing the necessary fields for changing the API key.</param>
    /// <returns>Returns an IActionResult indicating the result of the change API key operation.</returns>
    [HttpPost("change/apikey"), AuthByLogin]
    public virtual async Task<IActionResult> ChangeApiKey([FromBody] ChangeApiKeyDto changeKeyDto)
    {
        try
        {
            if (string.IsNullOrEmpty(changeKeyDto.OldApiKey) || string.IsNullOrEmpty(changeKeyDto.NewApiKey))
                return SingleResultWithError<AuthUserDto>("ApiKey must be specified");

            await AuthService.ChangeApiKey(changeKeyDto);
            await AuthService.SaveContext();

            Logger.LogInformation("ApiKey updated successfully");
            return NoContent();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }

    /// <summary>
    /// Creates an API key token for the logged-in user.
    /// </summary>
    /// <returns>Returns a <see cref="Microsoft.AspNetCore.Mvc.OkResult"/> with a status code of 204 (No Content) if the API key token creation is successful.</returns>
    /// <remarks>
    /// This method generates an API key token for the authenticated user and saves it to the database.
    /// If the API key token creation fails, the method returns a <see cref="ErrorResponse"/> with an error message.
    /// </remarks>
    [HttpPatch("create-api-key")]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 401)]
    [ProducesResponseType(204)]
    [AuthByLogin]
    public async Task<IActionResult> CreateApiKeyToken()
    {
        try
        {
            await AuthService.CreateApiKeyTokenForLoggedUser();
            await AuthService.SaveContext();
            return NoContent();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<AuthUserDto>(ex.Message);
        }
    }
}