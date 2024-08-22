using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// The base class for authentication queries.
/// </summary>
public abstract class AuthQuery : QueryBase
{
    /// <summary>
    /// Represents the Ids property of the AuthQuery class.
    /// </summary>
    /// <value>An array of long values representing the ids.</value>
    [JsonPropertyName("ids")] public long?[]? Ids { get; set; }
    /// <summary>
    /// Represents the username of a user.
    /// </summary>
    [JsonPropertyName("username")] public string? Username { get; set; }
    /// <summary>
    /// Gets or sets the API key token.
    /// </summary>
    /// <value>The API key token.</value>
    [JsonPropertyName("api_key")] public string? ApiKeyToken { get; set; }
}