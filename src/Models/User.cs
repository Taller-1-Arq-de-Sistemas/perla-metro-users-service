using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastNames { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;
        public Role Role { get; set; } = null!;
        public Guid RoleId { get; set; }
    }
}
