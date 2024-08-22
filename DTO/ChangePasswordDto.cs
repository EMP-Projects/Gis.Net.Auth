using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents the data transfer object for changing password.
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Gets or sets the ID of the object.
    /// </summary>
    [JsonPropertyName("id"), JsonIgnore] public long? Id { get; set; }

    /// <summary>
    /// Represents the old password in the <see cref="ChangePasswordDto"/> class.
    /// </summary>
    [JsonPropertyName("oldPassword"), Required] public string? OldPassword { get; set; }

    /// <summary>
    /// Represents the new password to be set for a user.
    /// </summary>
    /// <remarks>
    /// This property is used in the <see cref="ChangePasswordDto"/> class to specify the new password when changing the user's password.
    /// </remarks>
    [JsonPropertyName("newPassword"), Required] public string? NewPassword { get; set; }
}