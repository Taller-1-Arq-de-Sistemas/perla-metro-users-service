using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Data;

/// <summary>
/// Database context for the application.
/// </summary>
public class DataContext : DbContext
{
    /// <summary>
    /// Constructor for DataContext.
    /// </summary>
    /// <param name="options">The options to be used by a DbContext.</param>
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    /// <summary>
    /// The Users table in the database.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// The Roles table in the database.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// exposed in DbSet properties on your derived context. The resulting model may be
    /// cached and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("\"DeletedAt\" IS NULL");
    }
}
