using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Models;

/// <summary>
/// Represents a user role within the system.
/// </summary>
public class Role
{
    /// <summary>
    /// The unique identifier for the role.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// The name of the role.
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// The users associated with this role.
    /// </summary>
    public ICollection<User> Users { get; set; } = [];
}

