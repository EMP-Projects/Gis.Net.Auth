using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Services;

/// <summary>
/// Represents a service responsible for managing session information.
/// </summary>
public class SessionInfoService : ISessionInfoService
{
    /// <summary>
    /// Represents a service responsible for managing session information.
    /// </summary>
    public AuthUserDto? LoggedUser { get; set; }
}