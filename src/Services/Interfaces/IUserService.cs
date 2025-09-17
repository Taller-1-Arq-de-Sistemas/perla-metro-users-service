using PerlaMetroUsersService.DTOs.Users;

namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// Contract for user management operations (CRUD + queries).
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user with the specified role.
    /// </summary>
    /// <param name="user">User creation data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created user profile.</returns>
    /// <exception cref="Exceptions.DuplicateException">Thrown when another active user already uses the email.</exception>
    /// <exception cref="Exceptions.NotFoundException">Thrown when the provided role does not exist.</exception>
    Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct = default);
    /// <summary>
    /// Updates editable fields of the specified user.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="user">Updated data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <exception cref="Exceptions.NotFoundException">Thrown when the user is not found.</exception>
    Task Update(string id, EditUserRequestDto user, CancellationToken ct = default);
    /// <summary>
    /// Permanently deletes an existing user.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <exception cref="Exceptions.NotFoundException">Thrown when the user is not found.</exception>
    Task Delete(string id, CancellationToken ct = default);
    /// <summary>
    /// Soft-deletes an existing user.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <exception cref="Exceptions.NotFoundException">Thrown when the user is not found.</exception>
    /// <exception cref="Exceptions.ConflictException">Thrown when the user is already deleted.</exception>
    Task SoftDelete(string id, CancellationToken ct = default);

    /// <summary>
    /// Retrieves users with optional case-insensitive filters for name/email, and status filter (active/deleted/all).
    /// </summary>
    /// <param name="name">Optional full/partial name.</param>
    /// <param name="email">Optional full/partial email.</param>
    /// <param name="status">One of <c>active</c> (default), <c>deleted</c>, or <c>all</c>.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of users matching the filters.</returns>
    Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct = default);

    /// <summary>
    /// Retrieves a user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The user profile.</returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown when the user is not found or id is invalid.</exception>
    Task<GetUserResponseDto?> GetById(string id, CancellationToken ct = default);
}
