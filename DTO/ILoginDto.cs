namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents the data transfer object for login information.
/// </summary>
public interface ILoginDto
{
    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    /// <remarks>
    /// This property is used for authentication purposes.
    /// </remarks>
    string? Username { get; set; }

    /// <summary>
    /// Represents a password used for authentication.
    /// </summary>
    string? Password { get; set; }

    /// <summary>
    /// Represents the API key token for authentication.
    /// </summary>
    string? ApiKeyToken { get; set; }
}