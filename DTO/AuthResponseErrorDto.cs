using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class AuthResponseErrorDto
{
    [JsonPropertyName("hasError")] public bool HasError { get; set; }
    [JsonPropertyName("error")] public string? Error { get; set; }

    public AuthResponseErrorDto(string error)
    {
        Error = error;
        HasError = !string.IsNullOrEmpty(Error);
    }
}