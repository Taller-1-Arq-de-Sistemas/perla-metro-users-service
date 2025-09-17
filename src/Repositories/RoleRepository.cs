using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Data;
using PerlaMetroUsersService.Models;
using PerlaMetroUsersService.Repositories.Interfaces;

namespace PerlaMetroUsersService.Repositories;

/// <summary>
/// Repository for Role entities providing CRUD and lookup operations.
/// </summary>
public class RoleRepository : IRoleRepository
{
    private readonly DataContext _context;

    public RoleRepository(DataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public void Create(Role role) => _context.Roles.Add(role);

    /// <inheritdoc />
    public void Update(Role role) => _context.Roles.Update(role);

    /// <inheritdoc />
    public void Delete(Role role) => _context.Roles.Remove(role);

    /// <inheritdoc />
    public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken ct = default) =>
        await _context.Roles
            .AsNoTracking()
            .ToListAsync(ct);

    /// <inheritdoc />
    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id, ct);

    /// <inheritdoc />
    public async Task<Role?> GetByNameAsync(string name, CancellationToken ct = default) =>
        await _context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Name == name, ct);

    /// <inheritdoc />
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default) =>
        await _context.Roles
            .AnyAsync(r => r.Name == name, ct);
}
