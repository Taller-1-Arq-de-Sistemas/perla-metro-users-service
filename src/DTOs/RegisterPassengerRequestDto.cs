using System.ComponentModel.DataAnnotations;
using PerlaMetroUsersService.Validators.Auth;

namespace PerlaMetroUsersService.DTOs.Auth;

/// <summary>
/// DTO for registering a new passenger user.
/// </summary>
public sealed class RegisterPassengerRequestDto
{
    /// <summary>
    /// The first name of the passenger.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The last names of the passenger.
    /// </summary>
    [Required]
    [LastNames(ErrorMessage = "There must be two last names, without numbers or special characters.")]
    public string LastNames { get; set; } = null!;

    /// <summary>
    /// The email address of the passenger.
    /// </summary>
    [Required]
    [PerlaMetroEmail(ErrorMessage = "The email must be from Perla Metro domain.")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the passenger.
    /// </summary>
    [Required]
    [Password(ErrorMessage = "The password must be at least 8 characters long, with at least one uppercase letter, one lowercase letter, one number and one special character.")]
    public string Password { get; set; } = null!;
}
