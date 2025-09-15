namespace PerlaMetroUsersService.Repositories.Interfaces;

/// <summary>
/// Unit of Work interface for managing repositories and committing changes.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    IUserRepository Users { get; }
    /// <summary>
    /// Gets the role repository.
    /// </summary>
    IRoleRepository Roles { get; }
    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
