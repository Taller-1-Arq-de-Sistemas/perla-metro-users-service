namespace PerlaMetroUsersService.DTOs.Auth;

/// <summary>
/// DTO for registering a passenger and returning their details along with an authentication token.
/// </summary>
/// <param name="Id">The unique identifier of the registered passenger.</param>
/// <param name="Name">The first name of the registered passenger.</param>
/// <param name="LastNames">The last names of the registered passenger.</param>
/// <param name="Email">The email address of the registered passenger.</param>
/// <param name="Role">The role of the registered passenger.</param>
/// <param name="Token">The authentication token for the registered passenger.</param>
public sealed record RegisterPassengerResponseDto(
    string Id,
    string Name,
    string LastNames,
    string Email,
    string Role,
    string Token
);
