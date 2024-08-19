using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Entities;

[Table("users")]
public class ReducedUser : Login, IReducedUser
{
    [Column("firstname"), Required] public string? FirstName { get; set; } = string.Empty;
    
    [Column("lastname"), Required] public string? LastName { get; set; } = string.Empty;
    
    [Column("email"), Required] public string? Email { get; set; } = string.Empty;
}