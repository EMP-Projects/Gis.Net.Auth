using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class AuthResponseDto
{
    [JsonPropertyName("token")] public string? Token { get; set; }
    
    public AuthResponseDto(string token)
    {
        Token = token;
    }
}