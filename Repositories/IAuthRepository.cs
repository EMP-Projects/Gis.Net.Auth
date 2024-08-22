using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Repositories;

/// <summary>
/// Represents a repository for authentication operations.
/// </summary>
public interface IAuthRepository 
{
    /// <summary>
    /// Sign in method for authenticating a user.
    /// </summary>
    /// <param name="login">The login request containing the username and password or API key.</param>
    /// <returns>Returns an AuthUserDto object representing the authenticated user information.</returns>
    Task<AuthUserDto> SignIn(LoginRequestDto login);

    /// <summary>
    /// Signs up a new user.
    /// </summary>
    /// <param name="signUpDto">The signup information.</param>
    /// <returns>The authenticated user.</returns>
    Task<AuthUserDto> SignUp(ILoginDto signUpDto);

    /// <summary>
    /// Retrieves an authenticated user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The authenticated user with the specified ID.</returns>
    Task<AuthUserDto> GetAuthUserById(long id);

    /// <summary>
    /// Changes the password for the authenticated user.
    /// </summary>
    /// <param name="changePwdDto">The <see cref="ChangePasswordDto"/> object containing the old and new passwords.</param>
    /// <returns>
    /// Returns an <see cref="ILogin"/> object representing the updated login information.
    /// </returns>
    Task<ILogin> ChangePassword(ChangePasswordDto changePwdDto);

    /// <summary>
    /// Changes the API key for a login object.
    /// </summary>
    /// <param name="changeKeyDto">The ChangeApiKeyDto object containing the new and old API key values.</param>
    /// <returns>Returns the updated login object with the changed API key.</returns>
    Task<ILogin> ChangeApiKey(ChangeApiKeyDto changeKeyDto);

    /// <summary>
    /// Sets the credentials (username and password) for a user with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="username">The new username for the user.</param>
    /// <param name="password">The new password for the user.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetCredentials(long id, string username, string password);

    /// <summary>
    /// Resets the credentials (username and password) of the user with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    Task ResetCredentials(long id);
}