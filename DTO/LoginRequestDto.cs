using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Auth.DTO;

/// Represents a data transfer object for login requests.
/// /
public class LoginRequestDto : RequestBase, ILogin
{
    /// <summary>
    /// Represents the username associated with a user account.
    /// </summary>
    [JsonPropertyName("username")] public string? Username { get; set; }

    /// <summary>
    /// Represents a password property.
    /// </summary>
    [JsonPropertyName("password")] public string? Password { get; set; }

    /// <summary>
    /// Represents an API key token.
    /// </summary>
    [JsonPropertyName("apiKey")] public string? ApiKeyToken { get; set; }
}