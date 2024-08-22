using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Auth.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Auth.Entities;

/// <summary>
/// Login class represents a user login information.
/// </summary>
[Table("users")]
public class Login : ModelBase, ILogin
{
    /// <summary>
    /// Gets or sets the username of the login.
    /// </summary>
    /// <remarks>
    /// This property represents the username of the login entity.
    /// </remarks>
    [Column("username", TypeName = "varchar"), MaxLength(64)]
    public string? Username { get; set; }

    /// <summary>
    /// Represents the password of a user login.
    /// </summary>
    [Column("password", TypeName = "varchar")]
    public string? Password { get; set; }

    /// <summary>
    /// Represents the API key token of a user.
    /// </summary>
    [Column("api_key", TypeName = "varchar")]
    public string? ApiKeyToken { get; set; }
}