using PerlaMetroUsersService.Validators.Auth;

namespace PerlaMetroUsersService.DTOs.Users
{
    public class EditUserRequestDto
    {
        public string Name { get; set; } = null!;
        [LastNames(ErrorMessage = "There must be two last names, without numbers or special characters.")]
        public string LastNames { get; set; } = null!;
        [PerlaMetroEmail(ErrorMessage = "The email must be from Perla Metro domain.")]
        public string Email { get; set; } = null!;
        [Password(ErrorMessage = "The password must be at least 8 characters long, with at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; } = null!;
    }
}