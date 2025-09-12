using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Models;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Mappers.Users;

internal static class UsersWriteMappers
{
    public static User CreatePassenger(RegisterPassengerRequestDto dto, string roleId, IPasswordHasherService hasher, IClockService clock)
    {
        var normalizedEmail = NormalizeEmail(dto.Email);

        return new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name.Trim(),
            LastNames = dto.LastNames.Trim(),
            Email = normalizedEmail,
            Password = hasher.Hash(dto.Password),
            RoleId = roleId,
            CreatedAt = clock.UtcNow,
            DeletedAt = null
        };
    }

    public static User CreateUser(CreateUserRequestDto dto, string roleId, IPasswordHasherService hasher, IClockService clock)
    {
        var normalizedEmail = NormalizeEmail(dto.Email);

        return new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name.Trim(),
            LastNames = dto.LastNames.Trim(),
            Email = normalizedEmail,
            Password = hasher.Hash(dto.Password),
            RoleId = roleId,
            CreatedAt = clock.UtcNow,
            DeletedAt = null
        };
    }

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

    private static string NormalizeEmail(string e) => e.Trim().ToLowerInvariant();
}
