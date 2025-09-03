using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PerlaMetroUsersService.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Status { get; set; }
    }
}