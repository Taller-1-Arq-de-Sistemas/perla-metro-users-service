using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Data;
using PerlaMetroUsersService.Models;
using PerlaMetroUsersService.Repositories.Interfaces;

namespace PerlaMetroUsersService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(User user) => _context.Users.Add(user);

        public void Delete(User user) => _context.Users.Remove(user);

        public void Update(User user) => _context.Users.Update(user);

        public async Task<List<T>> GetAllAsync<T>(Expression<Func<User, T>> selector, CancellationToken ct = default) =>
            await GetAllAsync(selector, name: null, email: null, status: null, ct);

        public async Task<List<T>> GetAllAsync<T>(
            Expression<Func<User, T>> selector,
            string? name,
            string? email,
            string? status,
            CancellationToken ct = default)
        {
            var query = _context.Users.AsQueryable();

            if (status == "active")
                query = query.Where(u => u.DeletedAt == null);
            else if (status == "deleted")
                query = query.Where(u => u.DeletedAt != null);

            if (!string.IsNullOrWhiteSpace(name))
            {
                var n = name.Trim().ToLower();
                query = query.Where(u => (u.Name + " " + u.LastNames).Contains(n, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                var e = email.Trim().ToLower();
                query = query.Where(u => u.Email.Contains(e, StringComparison.CurrentCultureIgnoreCase));
            }

            return await query
                .AsNoTracking()
                .Select(selector)
                .ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync<T>(string id, Expression<Func<User, T>> selector, CancellationToken ct = default) =>
            await _context.Users
                .Where(u => u.Id == id && u.DeletedAt == null)
                .AsNoTracking()
                .Select(selector)
                .SingleOrDefaultAsync(ct);

        public async Task<User?> GetEntityByIdAsync(string id, CancellationToken ct = default) =>
            await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
            await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.DeletedAt == null, ct);

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default) =>
            await _context.Users
                .AnyAsync(u => u.Email == email && u.DeletedAt == null, ct);
    }
}
