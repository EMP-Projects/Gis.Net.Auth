using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents the response object for authentication.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// Represents a token used for authentication.
    /// </summary>
    [JsonPropertyName("token")] public string? Token { get; set; }

    /// <summary>
    /// Represents the response returned when a user is authenticated.
    /// </summary>
    public AuthResponseDto(string token)
    {
        Token = token;
    }
}