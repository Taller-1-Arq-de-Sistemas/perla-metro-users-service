using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Data;
using PerlaMetroUsersService.Models;
using PerlaMetroUsersService.Repositories.Interfaces;

namespace PerlaMetroUsersService.Repositories;

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
            var n = name.Trim();
            // Case-insensitive CONTAINS over Full Name using Postgres ILIKE
            query = query.Where(u => EF.Functions.ILike(u.Name + " " + u.LastNames, $"%{n}%"));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            var e = email.Trim();
            // Case-insensitive CONTAINS on email
            query = query.Where(u => EF.Functions.ILike(u.Email, $"%{e}%"));
        }

        return await query
            .AsNoTracking()
            .Select(selector)
            .ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync<T>(Guid id, Expression<Func<User, T>> selector, CancellationToken ct = default) =>
        await _context.Users
            .Where(u => u.Id == id && u.DeletedAt == null)
            .AsNoTracking()
            .Select(selector)
            .SingleOrDefaultAsync(ct);

    public async Task<User?> GetEntityByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null, ct);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.DeletedAt == null && EF.Functions.ILike(u.Email, email.Trim()), ct);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default) =>
        await _context.Users
            .AnyAsync(u => u.DeletedAt == null && EF.Functions.ILike(u.Email, email.Trim()), ct);
}
