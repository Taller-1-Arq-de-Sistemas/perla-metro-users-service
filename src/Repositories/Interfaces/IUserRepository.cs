using System.Linq.Expressions;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Repositories.Interfaces;

public interface IUserRepository
{
    void Create(User user);
    void Update(User user);
    void Delete(User user);
    Task<List<T>> GetAllAsync<T>(Expression<Func<User, T>> selector, CancellationToken ct = default);
    Task<List<T>> GetAllAsync<T>(Expression<Func<User, T>> selector, string? name, string? email, string? status, CancellationToken ct = default);
    Task<T?> GetByIdAsync<T>(string id, Expression<Func<User, T>> selector, CancellationToken ct = default);
    Task<User?> GetEntityByIdAsync(string id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
}
