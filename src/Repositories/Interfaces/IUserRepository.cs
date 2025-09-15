using System.Linq.Expressions;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Repositories.Interfaces;

/// <summary>
/// User repository interface for managing user entities.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    void Create(User user);
    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user to update.</param>
    void Update(User user);
    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    void Delete(User user);
    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="selector">The selector function to project the user data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A list of users.</returns>
    Task<List<T>> GetAllAsync<T>(Expression<Func<User, T>> selector, CancellationToken ct = default);
    /// <summary>
    /// Gets all users with optional filtering by name, email, and status.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="selector">The selector function to project the user data.</param>
    /// <param name="name">The name filter.</param>
    /// <param name="email">The email filter.</param>
    /// <param name="status">The status filter.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A list of users.</returns>
    Task<List<T>> GetAllAsync<T>(Expression<Func<User, T>> selector, string? name, string? email, string? status, CancellationToken ct = default);
    /// <summary>
    /// Gets a user by its ID.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="id">The ID of the user.</param>
    /// <param name="selector">The selector function to project the user data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The user with the specified ID, or null if not found.</returns>
    Task<T?> GetByIdAsync<T>(Guid id, Expression<Func<User, T>> selector, CancellationToken ct = default);
    /// <summary>
    /// Gets the user entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The user entity with the specified ID, or null if not found.</returns>
    Task<User?> GetEntityByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>
    /// Gets a user by its email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The user with the specified email, or null if not found.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    /// <summary>
    /// Checks if a user exists by its email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>True if the user exists, false otherwise.</returns>
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
}
