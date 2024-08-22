using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Services;

/// <summary>
/// Interface for the authentication service.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user in the authentication system.
    /// </summary>
    /// <param name="signUpDto">The user details for sign up.</param>
    /// <returns>
    /// The signed up user's authentication details (<see cref="AuthUserDto"/>),
    /// or null if the user registration failed.
    /// </returns>
    Task<AuthUserDto?> SignUp(ReducedUserDto signUpDto);

    /// <summary>
    /// Signs in a user with the provided login credentials.
    /// </summary>
    /// <param name="login">The login credentials.</param>
    /// <returns>The authenticated user.</returns>
    Task<AuthUserDto> SignIn(LoginDto login);

    /// <summary>
    /// Changes the password for the logged-in user.
    /// </summary>
    /// <param name="changePwdDto">The data transfer object for changing password.</param>
    /// <returns>The interface representing the updated login object.</returns>
    Task<ILogin> ChangePassword(ChangePasswordDto changePwdDto);

    /// <summary>
    /// Changes the API key for the logged user.
    /// </summary>
    /// <param name="changeKeyDto">The object containing the old and new API keys.</param>
    /// <returns>An object implementing the <see cref="ILogin"/> interface.</returns>
    Task<ILogin> ChangeApiKey(ChangeApiKeyDto changeKeyDto);

    /// <summary>
    /// Creates an API key token for the logged-in user.
    /// </summary>
    /// <returns>The created API key token.</returns>
    Task<string> CreateApiKeyTokenForLoggedUser();

    /// <summary>
    /// Saves the changes made to the context.
    /// </summary>
    /// <returns>The number of state entries written to the underlying database.</returns>
    /// <remarks>
    /// This method asynchronously saves all changes made to the context to the underlying database.
    /// It returns the number of state entries written to the database.
    /// </remarks>
    Task<int> SaveContext();

    /// <summary>
    /// Retrieves the user token for the given authentication user.
    /// </summary>
    /// <param name="authUser">The authentication user for which to retrieve the token.</param>
    /// <returns>The user token.</returns>
    Task<string> GetUserToken(AuthUserDto authUser);

    /// <summary>
    /// Sets the credentials for a user.
    /// </summary>
    /// <param name="login">The login information containing the username and password.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetCredentials(LoginDto login);

    /// <summary>
    /// Resets the credentials (username and password) for a user.
    /// </summary>
    /// <param name="login">The login information of the user.</param>
    /// <param name="checkLoggedUser">A flag indicating whether to check if the user is currently logged in. Default is true.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ResetCredentials(LoginDto login, bool checkLoggedUser = true);

    /// <summary>
    /// Gets or sets the name of the authorization header for login.
    /// </summary>
    string AuthorizationHeaderLogin { get; set; }
    /// <summary>
    /// Represents the property that contains the name of the header for API key authorization.
    /// </summary>
    string AuthorizationHeaderApiKey { get; set; }

    /// <summary>
    /// Extracts the token from the provided value.
    /// </summary>
    /// <param name="value">The value containing the token.</param>
    /// <param name="token">The extracted token.</param>
    /// <returns>True if the token is successfully extracted; otherwise, false.</returns>
    public bool ExtractToken(string value, out string? token);

    /// <summary>
    /// Checks the validity of the provided token and returns the corresponding authentication user.
    /// </summary>
    /// <param name="token">The token to be checked.</param>
    /// <returns>
    /// The <see cref="AuthUserDto"/> object representing the authentication user if the token is valid,
    /// otherwise null.
    /// </returns>
    public Task<AuthUserDto?> CheckToken(string token);

    /// <summary>
    /// Checks the validity of an API key.
    /// </summary>
    /// <param name="apiKey">The API key to check.</param>
    /// <returns>The authenticated user associated with the API key, or null if the key is invalid.</returns>
    public Task<AuthUserDto?> CheckApiKey(string apiKey);

    /// <summary>
    /// Gets or sets the currently logged-in user.
    /// </summary>
    /// <remarks>
    /// This property stores the information of the currently logged-in user.
    /// </remarks>
    AuthUserDto LoggedUser { get; set; }
}