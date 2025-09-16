using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Models;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Mappers.Users;

/// <summary>
/// Provides mapping methods for user write operations.
/// </summary>
internal static class UsersWriteMappers
{
    /// <summary>
    /// Creates a new User entity for a passenger registration.
    /// </summary>
    /// <param name="dto">The registration request data.</param>
    /// <param name="roleId">The role ID to assign to the user.</param>
    /// <param name="hasher">The password hasher service.</param>
    /// <param name="clock">The clock service.</param>
    /// <returns>A new User entity.</returns>
    public static User CreatePassenger(RegisterPassengerRequestDto dto, Guid roleId, IPasswordHasherService hasher, IClockService clock)
    {
        var normalizedEmail = NormalizeEmail(dto.Email);

        return new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            LastNames = dto.LastNames.Trim(),
            Email = normalizedEmail,
            Password = hasher.Hash(dto.Password),
            RoleId = roleId,
            CreatedAt = clock.UtcNow,
            DeletedAt = null
        };
    }

    /// <summary>
    /// Creates a new User entity from a CreateUserRequestDto.
    /// </summary>
    /// <param name="dto">The user creation request data.</param>
    /// <param name="roleId">The role ID to assign to the user.</param>
    /// <param name="hasher">The password hasher service.</param>
    /// <param name="clock">The clock service.</param>
    /// <returns>A new User entity.</returns>
    public static User CreateUser(CreateUserRequestDto dto, Guid roleId, IPasswordHasherService hasher, IClockService clock)
    {
        var normalizedEmail = NormalizeEmail(dto.Email);

        return new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            LastNames = dto.LastNames.Trim(),
            Email = normalizedEmail,
            Password = hasher.Hash(dto.Password),
            RoleId = roleId,
            CreatedAt = clock.UtcNow,
            DeletedAt = null
        };
    }

    /// <summary>
    /// Applies edits from an EditUserRequestDto to an existing User entity.
    /// </summary>
    /// <param name="dto">The user edit request data.</param>
    /// <param name="user"> The existing User entity to update.</param>
    /// <param name="hasher">The password hasher service.</param>
    public static void ApplyProfileEdit(EditUserRequestDto dto, User user, IPasswordHasherService hasher)
    {
        if (!string.IsNullOrWhiteSpace(dto.Name))
            user.Name = dto.Name.Trim();

        if (!string.IsNullOrWhiteSpace(dto.LastNames))
            user.LastNames = dto.LastNames.Trim();

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = NormalizeEmail(dto.Email);

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.Password = hasher.Hash(dto.Password);
    }

    /// <summary>
    /// Normalizes an email address by trimming whitespace and converting to lowercase.
    /// </summary>
    /// <param name="e">The email address to normalize.</param>
    /// <returns>The normalized email address.</returns>
    private static string NormalizeEmail(string e) => e.Trim().ToLowerInvariant();
}
