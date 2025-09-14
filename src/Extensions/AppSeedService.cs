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
            const int maxAttempts = 5;
            var delay = TimeSpan.FromSeconds(2);
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    context.Database.Migrate();
                    Seed.SeedData(context);
                    return;
                }
                catch (Exception ex)
                {
                    if (attempt == maxAttempts)
                    {
                        logger.LogError(ex, "Seeding failed after {Attempts} attempts", attempt);
                        return;
                    }
                    logger.LogWarning(ex, "Seeding attempt {Attempt} failed. Retrying in {Delay}s...", attempt, delay.TotalSeconds);
                    Thread.Sleep(delay);
                    delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 15));
                }
            }
        }
    }
}
