using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class ChangePasswordDto
{
    [JsonPropertyName("id"), JsonIgnore] public long? Id { get; set; }
    
    [JsonPropertyName("oldPassword"), Required] public string? OldPassword { get; set; }
    
    [JsonPropertyName("newPassword"), Required] public string? NewPassword { get; set; }
}