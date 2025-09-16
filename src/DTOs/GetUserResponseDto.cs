namespace PerlaMetroUsersService.DTOs.Users;

/// <summary>
/// DTO for getting detailed user information.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Name">The first name of the user.</param>
/// <param name="LastNames">The last names of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Role">The role of the user.</param>
/// <param name="Status">The status of the user.</param>
/// <param name="CreatedAt">The date and time when the user was created.</param>
public sealed record GetUserResponseDto(
    string Id,
    string Name,
    string LastNames,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt
);
