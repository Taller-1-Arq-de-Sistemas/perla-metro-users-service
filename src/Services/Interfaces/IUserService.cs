using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> UpdateUserAsync(string id, User user);
    Task<bool> DeleteUserAsync(string id);
}