using PerlaMetroUsersService.DTOs.Auth;

namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// Authentication service interface for user registration and login.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new passenger user.
    /// </summary>
    /// <param name="user">The user registration details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The response containing the registered user information.</returns>
    Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct = default);
    /// <summary>
    /// Logs in a user and returns a JWT token if successful.
    /// </summary>
    /// <param name="user">The user login details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The response containing the JWT token if login is successful.</returns>
    Task<LoginUserResponseDto?> Login(LoginUserRequestDto user, CancellationToken ct = default);
}
