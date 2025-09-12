using PerlaMetroUsersService.DTOs.Auth;

namespace PerlaMetroUsersService.Services.Interfaces;

public interface IAuthService
{
    Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct = default);
    Task<string?> Login(LoginUserRequestDto user, CancellationToken ct = default);
}