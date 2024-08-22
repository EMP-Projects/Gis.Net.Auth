using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class ChangeApiKeyDto
{
    /// <summary>
    /// Represents the ID of an entity.
    /// </summary>
    [JsonPropertyName("id"), JsonIgnore] public long? Id { get; set; }

    /// <summary>
    /// Represents the API key that needs to be changed.
    /// </summary>
    [JsonPropertyName("oldApiKey"), Required] public string? OldApiKey { get; set; }

    /// <summary>
    /// Gets or sets the new API key value.
    /// </summary>
    /// <remarks>
    /// The API key is used for authentication and authorization purposes.
    /// </remarks>
    [JsonPropertyName("newApiKey"), Required] public string? NewApiKey { get; set; }
}