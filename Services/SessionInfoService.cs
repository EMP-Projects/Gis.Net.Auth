using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Services;

/// <summary>
/// 
/// </summary>
public class SessionInfoService : ISessionInfoService
{
    /// <summary>
    /// Contiene i dati dell'utente correntemente loggato, se c'è
    /// </summary>
    public AuthUserDto? LoggedUser { get; set; }
}