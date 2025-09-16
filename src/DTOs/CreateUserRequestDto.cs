using System.ComponentModel.DataAnnotations;
using PerlaMetroUsersService.Validators.Auth;

namespace PerlaMetroUsersService.DTOs.Users;

/// <summary>
/// DTO for creating a new user.
/// </summary>
public sealed class CreateUserRequestDto
{
    /// <summary>
    /// The first name of the user.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The last names of the user.
    /// </summary>
    [Required]
    [LastNames(ErrorMessage = "There must be two last names, without numbers or special characters.")]
    public string LastNames { get; set; } = null!;

    /// <summary>
    /// The email address of the user.
    /// </summary>
    [Required]
    [PerlaMetroEmail(ErrorMessage = "The email must be from Perla Metro domain.")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the user.
    /// </summary>
    [Required]
    [Password(ErrorMessage = "The password must be at least 8 characters long, with at least one uppercase letter, one lowercase letter, one number and one special character.")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// The role of the user (either "admin" or "passenger").
    /// </summary>
    [RegularExpression("^(admin|passenger)$", ErrorMessage = "Role must be 'admin' or 'passenger'.")]
    public string Role { get; set; } = "passenger";
}
