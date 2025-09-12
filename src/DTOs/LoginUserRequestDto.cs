using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.DTOs.Auth
{
    public class LoginUserRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}