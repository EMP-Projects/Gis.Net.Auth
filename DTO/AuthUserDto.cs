using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Gis.Net.Auth.DTO;

public class AuthUserDto : DtoBase
{
    public Guid? Guid { get; set; }
        
    public string? Username { get; set; }

    [JsonIgnore]
    public string? Password { get; set; }
    public string? Token { get; set; }
    public string? DisplayText { get; set; }
    
    [JsonIgnore]
    public string? ApiKeyToken { get; set; }

    /// <summary>
    /// Genera il token Jwt
    /// </summary>
    /// <param name="secretKey"></param>
    public void GenerateJwtToken(string secretKey)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        Token = tokenHandler.WriteToken(token);
    }
        
    public void SetUserGuid(Guid userId, string username)
    {
        Guid = userId;
        Username = username;
    }
        
    public AuthUserDto() { }
    public AuthUserDto(Guid guid) => Guid = guid;
    public AuthUserDto(Guid guid, string username) : this(guid) => Username = username;
    public AuthUserDto(string username) => Username = username;
    public AuthUserDto(string username, string token) : this(username) => Token = token;
}