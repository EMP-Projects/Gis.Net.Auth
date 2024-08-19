using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Services;

public interface ISessionInfoService
{
    AuthUserDto? LoggedUser { get; set; }

    long LoggeUserId()
    {
        if (IsLogged() == false)
            throw new Exception($"La chiamata del metodo {nameof(LoggeUserId)} richiede che vi sia un utente loggato");
        return LoggedUser!.Id;
    }

    bool IsLogged() => LoggedUser is not null;
}