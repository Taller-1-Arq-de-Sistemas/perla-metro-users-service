using PerlaMetroUsersService.Data;
using PerlaMetroUsersService.Repositories.Interfaces;

namespace PerlaMetroUsersService.Repositories;

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

    public IUserRepository Users
    {
        get
        {
            _userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }

    public IRoleRepository Roles
    {
        get
        {
            _roleRepository ??= new RoleRepository(_context);
            return _roleRepository;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        await _context.SaveChangesAsync(ct);

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}