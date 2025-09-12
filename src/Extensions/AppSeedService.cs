using PerlaMetroUsersService.Data;
using Microsoft.EntityFrameworkCore;

namespace PerlaMetroUsersService.Extensions
{
    public static class AppSeedService
    {
        /// <summary>
        /// Calls the seed method to populate the database with example data.
        /// </summary>
        /// <param name="app">Singleton WebApplication</param>
        public static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                context.Database.Migrate();
                Seed.SeedData(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, " A problem ocurred during seeding ");
            }
        }
    }
}
