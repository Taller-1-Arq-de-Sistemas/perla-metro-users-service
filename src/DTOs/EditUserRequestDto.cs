using PerlaMetroUsersService.Validators.Auth;

namespace PerlaMetroUsersService.DTOs.Users;

/// <summary>
/// DTO for editing an existing user.
/// </summary>
public sealed class EditUserRequestDto
{
    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The last names of the user.
    /// </summary>
    [LastNames(ErrorMessage = "There must be two last names, without numbers or special characters.")]
    public string LastNames { get; set; } = null!;

    /// <summary>
    /// The email address of the user.
    /// </summary>
    [PerlaMetroEmail(ErrorMessage = "The email must be from Perla Metro domain.")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the user.
    /// </summary>
    [Password(ErrorMessage = "The password must be at least 8 characters long, with at least one uppercase letter, one lowercase letter, one number and one special character.")]
    public string Password { get; set; } = null!;
}
