using PerlaMetroUsersService.DTOs.Users;

namespace PerlaMetroUsersService.Services.Interfaces;

public interface IUserService
{
    Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct = default);
    Task Update(string id, EditUserRequestDto user, CancellationToken ct = default);
    Task Delete(string id, CancellationToken ct = default);
    Task SoftDelete(string id, CancellationToken ct = default);
    Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct = default);
    Task<GetUserResponseDto?> GetById(string id, CancellationToken ct = default);
}
