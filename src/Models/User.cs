using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Models;

/// <summary>
/// Represents a user within the system.
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// The last names of the user.
    /// </summary>
    public string LastNames { get; set; } = null!;
    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; } = null!;
    /// <summary>
    /// The password for the user.
    /// </summary>
    public string Password { get; set; } = null!;
    /// <summary>
    /// The date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// The date and time when the user was deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; } = null;
    /// <summary>
    /// The role associated with the user.
    /// </summary>
    public Role Role { get; set; } = null!;
    /// <summary>
    /// The unique identifier of the role associated with the user.
    /// </summary>
    public Guid RoleId { get; set; }
}

