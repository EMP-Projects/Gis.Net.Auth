using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Services;

/// <summary>
/// 
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="signUpDto"></param>
    /// <returns></returns>
    Task<AuthUserDto?> SignUp(ReducedUserDto signUpDto);
        
    /// <summary>
    /// Metodo chiamato dal controller per verificare la login, è il gateway per i vari metodi di autenticazione
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    Task<AuthUserDto> SignIn(LoginDto login);
    
    /// <summary>
    /// Metodo da implementare per fare la modifica password di un utente
    /// </summary>
    /// <param name="changePwdDto"></param>
    /// <returns></returns>
    Task<ILogin> ChangePassword(ChangePasswordDto changePwdDto);
        
    /// <summary>
    /// Metodo per modificare ApiKey collegato ad una utenza
    /// </summary>
    /// <param name="changeKeyDto"></param>
    /// <returns></returns>
    Task<ILogin> ChangeApiKey(ChangeApiKeyDto changeKeyDto);

    /// <summary>
    /// Generates a new API key token for the currently logged-in user.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, which upon completion returns the new API key token as a string.</returns>
    Task<string> CreateApiKeyTokenForLoggedUser();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<int> SaveContext();

    /// <summary>
    /// Generates a new JWT Token
    /// </summary>
    /// <returns></returns>
    Task<string> GetUserToken(AuthUserDto authUser);

    /// <summary>
    /// Imposta le nuove credenziali
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    Task SetCredentials(LoginDto login);

    /// <summary>
    /// Resetta le credenziali con stringhe vuote per Username e Password
    /// </summary>
    /// <param name="login"></param>
    /// <param name="checkLoggedUser">TRUE se si vuole controllare se username/password da resettare non corrispondono a utente collegato</param>
    /// <returns></returns>
    Task ResetCredentials(LoginDto login, bool checkLoggedUser = true);
    
    string AuthorizationHeaderLogin { get; set; }
    string AuthorizationHeaderApiKey { get; set; }

    public bool ExtractToken(string value, out string? token);
    public Task<AuthUserDto?> CheckToken(string token);
    public Task<AuthUserDto?> CheckApiKey(string apiKey);
    
    AuthUserDto LoggedUser { get; set; }
}