using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Mappers.Auth;

/// <summary>
/// Provides mapping methods for authentication responses.
/// </summary>
internal static class AuthResponseMappers
{
    /// <summary>
    /// Maps a User entity to a RegisterPassengerResponseDto.
    /// </summary>
    /// <param name="u">The user entity.</param>
    /// <param name="roleName">The name of the user's role.</param>
    /// <param name="token">The JWT token for the user.</param>
    /// <returns>A RegisterPassengerResponseDto representing the user.</returns>
    public static RegisterPassengerResponseDto ToRegisterResponse(User u, string roleName, string token)
        => new(
            u.Id.ToString(),
            u.Name,
            u.LastNames,
            u.Email,
            roleName,
            token
        );

    /// <summary>
    /// Maps a JWT token string to a LoginUserResponseDto.
    /// </summary>
    /// <param name="token">The JWT token for the user.</param>
    /// <returns>A LoginUserResponseDto representing the user.</returns>
    public static LoginUserResponseDto ToLoginResponse(string token)
        => new(token);
}
