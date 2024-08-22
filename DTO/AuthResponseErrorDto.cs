using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Class representing an authentication response error DTO.
/// </summary>
public class AuthResponseErrorDto
{
    /// <summary>
    /// Gets or sets a value indicating whether the response has an error.
    /// </summary>
    [JsonPropertyName("hasError")] public bool HasError { get; set; }
    /// <summary>
    /// Represents an error response returned by the authentication service.
    /// </summary>
    [JsonPropertyName("error")] public string? Error { get; set; }

    /// /
    public AuthResponseErrorDto(string error)
    {
        Error = error;
        HasError = !string.IsNullOrEmpty(Error);
    }
}