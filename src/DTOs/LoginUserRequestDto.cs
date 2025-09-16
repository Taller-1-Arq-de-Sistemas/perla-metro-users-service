using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.DTOs.Auth;

/// <summary>
/// DTO for user login request.
/// </summary>
public sealed class LoginUserRequestDto
{
    /// <summary>
    /// The email address of the user.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the user.
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}
