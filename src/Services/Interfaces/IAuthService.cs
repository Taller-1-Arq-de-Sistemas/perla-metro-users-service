using PerlaMetroUsersService.DTOs.Auth;

namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// Authentication service interface for user registration and login.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new passenger user and issues a JWT.
    /// </summary>
    /// <param name="user">Registration data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Created user profile plus token.</returns>
    /// <exception cref="PerlaMetroUsersService.Exceptions.DuplicateException">Thrown when a user with the same email already exists.</exception>
    /// <exception cref="PerlaMetroUsersService.Exceptions.NotFoundException">Thrown when the default role cannot be found.</exception>
    Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct = default);
    /// <summary>
    /// Authenticates a user by email and password and issues a JWT.
    /// </summary>
    /// <param name="user">Login credentials.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Login response with token.</returns>
    /// <exception cref="System.Security.Authentication.InvalidCredentialException">Thrown when credentials are invalid.</exception>
    /// <exception cref="PerlaMetroUsersService.Exceptions.NotFoundException">Thrown when the user's role cannot be resolved.</exception>
    Task<LoginUserResponseDto?> Login(LoginUserRequestDto user, CancellationToken ct = default);
}
