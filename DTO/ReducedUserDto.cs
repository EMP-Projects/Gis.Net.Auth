using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class ReducedUserDto : LoginDto, IReducedUser
{
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    [JsonPropertyName("email")] public string? Email { get; set; }
}