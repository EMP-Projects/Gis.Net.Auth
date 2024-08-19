using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Auth.DTO;

public class LoginRequestDto : RequestBase, ILogin
{
    [JsonPropertyName("username")] public string? Username { get; set; }
    
    [JsonPropertyName("password")] public string? Password { get; set; }
    
    [JsonPropertyName("apiKey")] public string? ApiKeyToken { get; set; }
}