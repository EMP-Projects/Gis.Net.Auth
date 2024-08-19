namespace Gis.Net.Auth.DTO;

public interface ILoginDto
{
    string? Username { get; set; }
    
    string? Password { get; set; }
    
    string? ApiKeyToken { get; set; }
}