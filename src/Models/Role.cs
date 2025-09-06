using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = [];
    }
}