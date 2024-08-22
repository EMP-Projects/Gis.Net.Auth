namespace Gis.Net.Auth.DTO;

/// <summary>
/// The IReducedUser interface represents a reduced user object with basic information.
/// </summary>
public interface IReducedUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    /// <value>
    /// The last name of the user.
    /// </value>
    string? LastName { get; set; }

    /// <summary>
    /// Represents the email property.
    /// </summary>
    string? Email { get; set; }
}