using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Repositories.Interfaces;

public interface IRoleRepository
{
    void Create(Role role);
    void Update(Role role);
    void Delete(Role role);
    Task<IEnumerable<Role>> GetAllAsync(CancellationToken ct = default);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Role?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
}
