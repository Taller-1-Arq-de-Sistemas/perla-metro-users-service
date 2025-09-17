using PerlaMetroUsersService.Data;
using PerlaMetroUsersService.Repositories.Interfaces;

namespace PerlaMetroUsersService.Repositories;

/// <summary>
/// Coordinates repository operations and a shared DbContext to ensure atomic commits.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private IUserRepository _userRepository = null!;
    private IRoleRepository _roleRepository = null!;
    private bool _disposed = false;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Repository for user entities.
    /// </summary>
    public IUserRepository Users
    {
        get
        {
            _userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }

    /// <summary>
    /// Repository for role entities.
    /// </summary>
    public IRoleRepository Roles
    {
        get
        {
            _roleRepository ??= new RoleRepository(_context);
            return _roleRepository;
        }
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        await _context.SaveChangesAsync(ct);

    /// <summary>
    /// Disposes the underlying DbContext.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();

        _disposed = true;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
