using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents a data transfer object for a reduced user with basic information.
/// Inherits from the LoginDto class and implements the IReducedUser interface.
/// </summary>
/// <remarks>
/// A ReducedUserDto object contains information about a user, such as their first name,
/// last name, and email address. It is used for transferring user data between different
/// layers of an application.
/// </remarks>
public class ReducedUserDto : LoginDto, IReducedUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    /// <remarks>
    /// This property represents the first name of the user in a reduced user object.
    /// It is used to store and retrieve the first name of the user.
    /// </remarks>
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    
    /// <summary>
    /// Represents an email property.
    /// </summary>
    /// <remarks>
    /// This property is declared within the class <see cref="ReducedUserDto"/> and implements the interface <see cref="IReducedUser"/>.
    /// The email property represents the email address of a user.
    /// </remarks>
    [JsonPropertyName("email")] public string? Email { get; set; }
}