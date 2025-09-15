using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing Role entities.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="role">The role to create.</param>
    void Create(Role role);
    /// <summary>
    /// Updates an existing role.
    /// </summary>
    /// <param name="role">The role to update.</param>
    void Update(Role role);
    /// <summary>
    /// Deletes a role.
    /// </summary>
    /// <param name="role">The role to delete.</param>
    void Delete(Role role);
    /// <summary>
    /// Gets all roles.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    Task<IEnumerable<Role>> GetAllAsync(CancellationToken ct = default);
    /// <summary>
    /// Gets a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role.</param>
    /// <param name="ct">Cancellation token.</param>
    Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>
    /// Gets a role by its name.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <param name="ct">Cancellation token.</param>
    Task<Role?> GetByNameAsync(string name, CancellationToken ct = default);
    /// <summary>
    /// Checks if a role exists by its name.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns></returns>
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
}
