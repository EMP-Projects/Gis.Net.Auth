using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Auth.DTO;

public abstract class AuthQuery : QueryBase
{
    [JsonPropertyName("ids")] public long?[]? Ids { get; set; }
    [JsonPropertyName("username")] public string? Username { get; set; }
    [JsonPropertyName("api_key")] public string? ApiKeyToken { get; set; }
}