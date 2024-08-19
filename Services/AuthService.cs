using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gis.Net.Auth.DTO;
using Gis.Net.Auth.Repositories;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Gis.Net.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Gis.Net.Auth.Services;

/// <inheritdoc cref="Gis.Net.Auth.Services.IAuthService" />
public abstract class AuthService<TModel, TDto, TQuery, TRequest, TContext> : 
    ServiceCore<TModel, TDto, TQuery, TRequest, TContext>, IAuthService
    where TModel : ModelBase, ILogin
    where TDto : DtoBase, ILoginDto
    where TQuery : AuthQuery, new()
    where TRequest : RequestBase
    where TContext : DbContext
{
    private readonly IAuthRepository _repository;
    private readonly ILogger _logger;
    private readonly ISessionInfoService _session;
    private readonly IConfiguration _configuration;

    public string AuthorizationHeaderLogin { get; set; } = "Authorization";
    public string AuthorizationHeaderApiKey { get; set; } = "X-API-Key";

    /// <inheritdoc />
    protected AuthService(ILogger logger, IRepositoryCore<TModel, TDto, TQuery, TContext> repositoryCore, IConfiguration configuration)
        : base(logger, repositoryCore)
    {
        _configuration = configuration;
    }

    private void CheckIfLoggedUser(long id)
    {
        if (_session.IsLogged() && _session.LoggedUser?.Id == id)
            throw new Exception("it is not possible to change the credentials of the logged in user");
    }

    /// <inheritdoc />
    public async Task SetCredentials(LoginDto login)
    {
        // non posso modificare le credenziali dell'utente collegato
        CheckIfLoggedUser(login.Id);

        if (login.Username is null || login.Password is null)
            throw new Exception("E' necessario specificare username e password");
        
        await _repository.SetCredentials(login.Id, login.Username, login.Password);
    }

    /// <inheritdoc />
    public async Task ResetCredentials(LoginDto login, bool checkLoggedUser = true)
    {
        // non posso modificare le credenziali dell'utente collegato
        if (checkLoggedUser) CheckIfLoggedUser(login.Id);
        // solo se il valore nel dto è uguale a key allora la cancellazione è intenzionale
        Logger.LogDebug($"Resetto il campo username e password per l'utente con id {login.Id}");
        await _repository.ResetCredentials(login.Id);
    }

    /// <summary>
    /// An instance of <see cref="AuthUserDto"/> for the logged user
    /// </summary>
    public AuthUserDto? LoggedUser
    {
        get => _session.LoggedUser;
        set => _session.LoggedUser = value;
    }

    /// <inheritdoc />
    public virtual async Task<AuthUserDto> Login(string username, string password)
    {
        _session.LoggedUser = await _repository.SignIn(new LoginRequestDto { Username = username, Password = password });
        return _session.LoggedUser;
    }

    public virtual bool ExtractToken(string value, out string? token)
    {
        var tokenValues = value.Split(' ');
        token = tokenValues.Length > 0 ? tokenValues[^1] : null;
        return token is not null;
    }

    /// <inheritdoc />
    public async Task<AuthUserDto?> CheckToken(string token)
    {
        var userId = GetUserIdFromJwtToken(token);
        if (userId is null)
            return null;
        var user = await _repository.GetAuthUserById(userId.Value);
        return user;
    }
    
    public async Task<AuthUserDto?> CheckApiKey(string apiKey) => await _repository.SignIn(new LoginRequestDto { ApiKeyToken = apiKey });
    
    /// <inheritdoc />
    public virtual async Task<AuthUserDto?> SignUp(ReducedUserDto signUpDto)
    {
        var authUser = await _repository.SignUp(signUpDto);
        _session.LoggedUser = authUser;
        return authUser;
    }

    /// <inheritdoc />
    public async Task<AuthUserDto> SignIn(LoginDto login)
    {
        if (login is { IsNullLoginByPwd: true, IsNullLoginByKey: true })
            throw new Exception("LoginDto misses required fields");
        
        var userDto =  !login.IsNullLoginByPwd 
            ? await _repository.SignIn(new LoginRequestDto { Username = login.Username, Password = login.Password }) 
            : await _repository.SignIn(new LoginRequestDto { ApiKeyToken = login.ApiKeyToken });

        _session.LoggedUser = userDto;
        
        return userDto;
    }

    /// <inheritdoc />
    public virtual async Task<ILogin> ChangePassword(ChangePasswordDto changePwdDto)
    {
        if (!_session.IsLogged() || _session.LoggedUser?.Id is null)
            throw new Exception("It is not possible to change the Password");

        changePwdDto.Id = _session.LoggedUser.Id;
        return await _repository.ChangePassword(changePwdDto);
    }

    /// <inheritdoc />
    public virtual async Task<ILogin> ChangeApiKey(ChangeApiKeyDto changeKeyDto)
    {
        if (!_session.IsLogged() || _session.LoggedUser?.Id is null)
            throw new Exception("It is not possible to change ApiKey");

        changeKeyDto.Id = _session.LoggedUser.Id;
        return await _repository.ChangeApiKey(changeKeyDto);
    }

    /// <inheritdoc />
    public virtual async Task<int> SaveContext() => await GetRepository().SaveChanges();

    protected virtual string JwtSecret { get; set; } = "JwtBe4rerSecre7KEY";
    protected virtual TimeSpan JwtDuration { get; set; } = TimeSpan.FromMinutes(60 * 18);
    
    /// <summary>
    /// Generates a JWT (JSON Web Token) for the given authenticated user.
    /// </summary>
    /// <param name="authUser">The authenticated user for whom the JWT is being generated.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the generated JWT as a string.</returns>
    protected Task<string> GenerateJwtToken(AuthUserDto authUser)
    {
        // Calculate the expiration time for the JWT based on the current time and the duration specified in the configuration.
        var expiration = DateTime.Now.Add(JwtDuration);

        // Log the expiration time of the JWT for informational purposes.
        Logger.LogInformation($"Jwt Token will expire at {expiration:f}");

        // Generate the JWT using the authenticated user information and the calculated expiration time.
        var token = GenerateJwtToken(authUser, expiration);

        // Return the generated JWT as a completed task.
        return Task.FromResult(token);
    }
    
    private string GenerateJwtToken(AuthUserDto user, DateTime expiration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = expiration,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    protected long? GetUserIdFromJwtToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.UTF8.GetBytes(JwtSecret);
        try
        {
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                out var validatedToken
            );

            var jwtToken = (JwtSecurityToken)validatedToken;
            var idClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "id");
            if (idClaim is null)
            {
                Logger.LogError("Unable to find claim in Jwt Token");
                return null;
            }

            if (long.TryParse(idClaim.Value, out var id))
            {
                return id;
            }

            Logger.LogError($"Id value not valid in user claim: expected number, found {idClaim.Value}");
        }
        catch (Exception e)
        {
            Logger.LogError($"Exception in {nameof(GetUserIdFromJwtToken)}");
            Logger.LogError(e.Message);
            Logger.LogError(e.StackTrace);
        }

        return null;
    }

    /// <inheritdoc />
    public Task<string> GetUserToken(AuthUserDto authUser) => GenerateJwtToken(authUser);

    /// <inheritdoc />
    public async Task<string> CreateApiKeyTokenForLoggedUser()
    {
        if (LoggedUser is null)
            throw new Exception( $"LoggedUser is null in {nameof(CreateApiKeyTokenForLoggedUser)}");

        var dto = await GetRepository().Find(LoggedUser.Id);
        if (dto is null)
            throw new Exception($"Find failed in {nameof(CreateApiKeyTokenForLoggedUser)}");

        var token = Guid.NewGuid().ToString();
        _logger.LogInformation($"Creating ApiKey token ${token} for user ${LoggedUser.Username}");

        await GetRepository()
            .Update(dto, new UpdateOptions<TModel, TDto, TQuery>
                {
                    OnExtraMapping = (userDto, model) => model.ApiKeyToken = PasswordHasher.HashPasswordV3(token)
                }
            );
        return token;
    }
}
