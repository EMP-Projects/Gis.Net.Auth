using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Auth.DTO;

public class LoginDto : DtoBase, ILoginDto
{
    [JsonPropertyName("username")] public string? Username { get; set; }

    [JsonIgnore] public string? Password { get; set; }

    [JsonIgnore] public string? HashedPassword { get; set; }

    [JsonIgnore] public string? ApiKeyToken { get; set; }

    [JsonIgnore] public string? HashedApiKeyToken { get; set; }

    [JsonIgnore]
    public bool IsNullLoginByPwd => string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password);

    [JsonIgnore]
    public bool IsNullLoginByKey => string.IsNullOrEmpty(ApiKeyToken);
}