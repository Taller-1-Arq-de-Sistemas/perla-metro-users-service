using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
    }
}