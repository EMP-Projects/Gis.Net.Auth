namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents the interface for a login object.
/// </summary>
public interface ILogin
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    /// <remarks>
    /// This property represents the username for a user.
    /// </remarks>
    string Username { get; set; }

    /// <summary>
    /// Represents the password associated with a user.
    /// </summary>
    string Password { get; set; }

    /// <summary>
    /// Represents an API key token used for authentication.
    /// </summary>
    string? ApiKeyToken { get; set; }
}