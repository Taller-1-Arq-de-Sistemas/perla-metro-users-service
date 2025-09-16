using System.Linq.Expressions;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Mappers.Users;

/// <summary>
/// Provides mapping methods for user read operations.
/// </summary>
internal static class UsersReadMappers
{
    /// <summary>
    /// Maps a User entity to a ListUserResponseDto.
    /// </summary>
    public static readonly Expression<Func<User, ListUserResponseDto>> ToListItem =
        u => new ListUserResponseDto(
            u.Id.ToString(),
            u.Name + " " + u.LastNames,
            u.Email,
            u.DeletedAt == null ? "Active" : "Deleted",
            u.CreatedAt
        );

    /// <summary>
    /// Maps a User entity to a GetUserResponseDto.
    /// </summary>
    public static readonly Expression<Func<User, GetUserResponseDto>> ToDetail =
        u => new GetUserResponseDto(
            u.Id.ToString(),
            u.Name,
            u.LastNames,
            u.Email,
            u.Role!.Name,
            u.DeletedAt == null ? "Active" : "Deleted",
            u.CreatedAt
        );
}
