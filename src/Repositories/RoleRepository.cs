using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Data;
using PerlaMetroUsersService.Models;
using PerlaMetroUsersService.Repositories.Interfaces;

namespace PerlaMetroUsersService.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Role role) => _context.Roles.Add(role);

        public void Update(Role role) => _context.Roles.Update(role);

        public void Delete(Role role) => _context.Roles.Remove(role);

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken ct = default) =>
            await _context.Roles
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            await _context.Roles
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == id, ct);

        public async Task<Role?> GetByNameAsync(string name, CancellationToken ct = default) =>
            await _context.Roles
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Name == name, ct);

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default) =>
            await _context.Roles
                .AnyAsync(r => r.Name == name, ct);
    }
}
