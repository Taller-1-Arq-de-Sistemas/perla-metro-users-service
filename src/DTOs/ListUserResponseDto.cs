namespace PerlaMetroUsersService.DTOs.Users;

public sealed class ListUserResponseDto
{
    public string Id { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}
