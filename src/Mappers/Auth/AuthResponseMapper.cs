using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Mappers.Auth;

internal static class AuthResponseMappers
{
    public static RegisterPassengerResponseDto ToRegisterResponse(User u, string roleName, string token)
        => new()
        {
            Id = u.Id,
            Name = u.Name,
            LastNames = u.LastNames,
            Email = u.Email,
            Role = roleName,
            Token = token,
        };

    public static LoginUserResponseDto ToLoginResponse(string token)
        => new()
        {
            Token = token,
        };
}
