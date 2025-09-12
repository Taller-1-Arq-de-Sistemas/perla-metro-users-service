namespace PerlaMetroUsersService.DTOs.Users;

public sealed class GetUserResponseDto
{
    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string LastNames { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Role { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}
