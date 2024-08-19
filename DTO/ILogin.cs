namespace Gis.Net.Auth.DTO;

public interface ILogin
{
    string Username { get; set; }

    string Password { get; set; }

    string? ApiKeyToken { get; set; }
}