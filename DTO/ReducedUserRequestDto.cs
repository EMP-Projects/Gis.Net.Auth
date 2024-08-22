using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents a data transfer object for a reduced user request.
/// </summary>
public class ReducedUserRequestDto : LoginRequestDto, IReducedUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    /// <remarks>
    /// This property is used to store the last name of a user.
    /// </remarks>
    /// <value>The last name of the user.</value>
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    
    /// <summary>
    /// Represents the email property of a ReducedUserRequestDto object.
    /// </summary>
    /// <remarks>
    /// This property stores the email address of a user.
    /// </remarks>
    [JsonPropertyName("email")] public string? Email { get; set; }
}