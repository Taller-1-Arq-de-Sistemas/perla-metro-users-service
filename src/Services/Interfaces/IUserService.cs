using PerlaMetroUsersService.DTOs.Users;

namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// User service interface for managing user operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user registration details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The response containing the created user information.</returns>
    Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct = default);
    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user information.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task Update(string id, EditUserRequestDto user, CancellationToken ct = default);
    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task Delete(string id, CancellationToken ct = default);
    /// <summary>
    /// Soft deletes a user.
    /// </summary>
    /// <param name="id">The ID of the user to soft delete.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task SoftDelete(string id, CancellationToken ct = default);
    /// <summary>
    /// Gets all users with optional filtering by name, email, and status.
    /// </summary>
    /// <param name="name">The name filter.</param>
    /// <param name="email">The email filter.</param>
    /// <param name="status">The status filter.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The response containing the list of users.</returns>
    Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct = default);
    /// <summary>
    /// Gets a user by its ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The response containing the user information.</returns>
    Task<GetUserResponseDto?> GetById(string id, CancellationToken ct = default);
}
