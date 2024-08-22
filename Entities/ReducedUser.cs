using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Entities;

/// <summary>
/// The ReducedUser class represents a user object with reduced information such as first name, last name, and email.
/// </summary>
[Table("users")]
public class ReducedUser : Login, IReducedUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Column("firstname"), Required] public string? FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Column("lastname"), Required] public string? LastName { get; set; } = string.Empty;

    /// <summary>
    /// Email represents the email address of a user.
    /// </summary>
    /// <remarks>
    /// This property is used to store the email address of a user. It is a required field.
    /// </remarks>
    [Column("email"), Required] public string? Email { get; set; } = string.Empty;
}