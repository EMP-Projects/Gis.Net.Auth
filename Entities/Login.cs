using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Auth.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Auth.Entities;

[Table("users")]
public class Login : ModelBase, ILogin
{
    [Column("username", TypeName = "varchar"), MaxLength(64)]
    public string? Username { get; set; }
    
    [Column("password", TypeName = "varchar")]
    public string? Password { get; set; }
    
    [Column("api_key", TypeName = "varchar")]
    public string? ApiKeyToken { get; set; }
}