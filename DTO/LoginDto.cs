using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents the data transfer object for login information.
/// </summary>
public class LoginDto : DtoBase, ILoginDto
{
    /// <summary>
    /// Represents the username of a user for authentication purposes.
    /// </summary>
    [JsonPropertyName("username")] public string? Username { get; set; }

    /// <summary>
    /// Represents a password property for authentication purposes.
    /// </summary>
    [JsonIgnore] public string? Password { get; set; }

    /// <summary>
    /// Represents the hashed password of a user.
    /// </summary>
    [JsonIgnore] public string? HashedPassword { get; set; }

    /// <summary>
    /// Represents the API key token used for authentication.
    /// </summary>
    [JsonIgnore] public string? ApiKeyToken { get; set; }

    /// <summary>
    /// Represents the hashed API key token for a login.
    /// </summary>
    [JsonIgnore] public string? HashedApiKeyToken { get; set; }

    /// <summary>
    /// Gets a value indicating whether the login information is null or empty.
    /// </summary>
    [JsonIgnore]
    public bool IsNullLoginByPwd => string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password);

    /// <summary>
    /// Gets a value indicating whether the ApiKeyToken property is null or empty.
    /// </summary>
    /// <value>
    /// <c>true</c> if ApiKeyToken is null or empty; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool IsNullLoginByKey => string.IsNullOrEmpty(ApiKeyToken);
}