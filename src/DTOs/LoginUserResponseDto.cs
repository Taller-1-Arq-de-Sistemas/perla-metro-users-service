namespace PerlaMetroUsersService.DTOs.Auth;

/// <summary>
/// DTO for login user response.
/// </summary>
/// <param name="Token">The JWT token for the authenticated user.</param>
public sealed record LoginUserResponseDto(
    string Token
);
