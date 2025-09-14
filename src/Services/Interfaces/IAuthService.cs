using PerlaMetroUsersService.DTOs.Auth;

namespace PerlaMetroUsersService.Services.Interfaces;

public interface IAuthService
{
    Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct = default);
    Task<LoginUserResponseDto?> Login(LoginUserRequestDto user, CancellationToken ct = default);
}