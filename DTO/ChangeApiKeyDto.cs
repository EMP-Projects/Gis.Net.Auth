using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class ChangeApiKeyDto
{
    [JsonPropertyName("id"), JsonIgnore] public long? Id { get; set; }
    
    [JsonPropertyName("oldApiKey"), Required] public string? OldApiKey { get; set; }
    
    [JsonPropertyName("newApiKey"), Required] public string? NewApiKey { get; set; }
}