using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.DTOs.Users
{
    public class ListUsersQueryDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        [RegularExpression("^(active|deleted|all)$", ErrorMessage = "Status must be 'active', 'deleted', or 'all'.")]
        public string? Status { get; set; }
    }
}

