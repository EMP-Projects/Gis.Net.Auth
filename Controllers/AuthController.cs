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
    /// 
    /// </summary>
    /// <returns></returns>
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