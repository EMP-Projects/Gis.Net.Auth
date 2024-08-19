namespace Gis.Net.Auth.DTO;

public interface IReducedUser
{
    string? FirstName { get; set; }
    
    string? LastName { get; set; }
    
    string? Email { get; set; }
}