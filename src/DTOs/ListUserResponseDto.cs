namespace PerlaMetroUsersService.DTOs.Users;

/// <summary>
/// DTO for listing user information.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="FullName">The full name of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Status">The status of the user.</param>
/// <param name="CreatedAt">The date and time when the user was created.</param>
public sealed record ListUserResponseDto(
    string Id,
    string FullName,
    string Email,
    string Status,
    DateTime CreatedAt
);
