using System.Linq.Expressions;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Mappers.Users;

internal static class UsersReadMappers
{
    public static readonly Expression<Func<User, ListUserResponseDto>> ToListItem =
        u => new ListUserResponseDto
        {
            Id = u.Id.ToString(),
            FullName = u.Name + " " + u.LastNames,
            Email = u.Email,
            Status = u.DeletedAt == null ? "Active" : "Deleted",
            CreatedAt = u.CreatedAt,
        };

    public static readonly Expression<Func<User, GetUserResponseDto>> ToDetail =
        u => new GetUserResponseDto
        {
            Id = u.Id.ToString(),
            Name = u.Name,
            LastNames = u.LastNames,
            Email = u.Email,
            Role = u.Role!.Name,
            Status = u.DeletedAt == null ? "Active" : "Deleted",
            CreatedAt = u.CreatedAt,
        };

    public static GetUserResponseDto MapToDetail(User u) =>
        new()
        {
            Id = u.Id.ToString(),
            Name = u.Name,
            LastNames = u.LastNames,
            Email = u.Email,
            Role = u.Role!.Name,
            Status = u.DeletedAt == null ? "Active" : "Deleted",
            CreatedAt = u.CreatedAt,
        };
}
