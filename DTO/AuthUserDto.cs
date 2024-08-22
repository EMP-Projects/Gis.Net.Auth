using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Data transfer object representing authentication user information.
/// </summary>
public class AuthUserDto : DtoBase
{
    /// <summary>
    /// Represents a globally unique identifier (GUID).
    /// </summary>
    public Guid? Guid { get; set; }

    /// <summary>
    /// Gets or sets the username of the authenticated user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Represents the password of an authentication user.
    /// </summary>
    [JsonIgnore]
    public string? Password { get; set; }
    public string? Token { get; set; }
    /// <summary>
    /// Gets or sets the display text for the user.
    /// </summary>
    public string? DisplayText { get; set; }

    /// <summary>
    /// Represents the API key token used for authentication.
    /// </summary>
    [JsonIgnore]
    public string? ApiKeyToken { get; set; }

    /// <summary>
    /// Generates a Jwt token.
    /// </summary>
    /// <param name="secretKey">The secret key used for token generation.</param>
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

    /// <summary>
    /// Sets the Guid and Username properties of the AuthUserDto.
    /// </summary>
    /// <param name="userId">The Guid to set as the Guid property.</param>
    /// <param name="username">The string value to set as the Username property.</param>
    public void SetUserGuid(Guid userId, string username)
    {
        Guid = userId;
        Username = username;
    }

    /// <summary>
    /// Represents an authenticated user.
    /// </summary>
    public AuthUserDto() { }
    public AuthUserDto(Guid guid) => Guid = guid;

    /// <summary>
    /// Data transfer object (DTO) for an authenticated user.
    /// </summary>
    public AuthUserDto(Guid guid, string username) : this(guid) => Username = username;

    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an authenticated user.
    /// </summary>
    public AuthUserDto(string username) => Username = username;

    /// <summary>
    /// AuthUserDto represents a data transfer object for authentication user data.
    /// It inherits from DtoBase class.
    /// </summary>
    public AuthUserDto(string username, string token) : this(username) => Token = token;
}