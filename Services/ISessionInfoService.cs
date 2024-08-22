using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Services;

/// <summary>
/// Represents a service responsible for managing session information.
/// </summary>
public interface ISessionInfoService
{
    /// <summary>
    /// Represents the logged-in user information.
    /// </summary>
    AuthUserDto? LoggedUser { get; set; }

    /// <summary>
    /// Retrieves the ID of the logged-in user.
    /// </summary>
    /// <returns>The ID of the logged-in user.</returns>
    long LoggeUserId()
    {
        if (IsLogged() == false)
            throw new Exception($"Calling the {nameof(LoggeUserId)} method requires that there is a logged in user");
        return LoggedUser!.Id;
    }

    /// <summary>
    /// Determines whether a user is logged in.
    /// </summary>
    /// <returns><c>true</c> if a user is logged in; otherwise, <c>false</c>.</returns>
    bool IsLogged() => LoggedUser is not null;
}