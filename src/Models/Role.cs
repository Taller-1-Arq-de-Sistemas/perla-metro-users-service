using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = [];
    }
}
