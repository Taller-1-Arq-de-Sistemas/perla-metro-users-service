using System.Text.Json;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Data
{
    public class Seed
    {
        /// <summary>
        /// Seed the database with examples models in the json files if the database is empty.
        /// </summary>
        /// <param name="context">Database Context </param>
        public static void SeedData(DataContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            SeedRoles(context, options);
            SeedUsers(context, options);
        }

        /// <summary>
        /// Seed the database with the roles in the json file, then save changes in the database.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedRoles(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Roles?.Any();
            if (result is true or null) return;

            var path = "src/Data/Seeders/RolesData.json";
            var rolesData = File.ReadAllText(path);
            var rolesList = JsonSerializer.Deserialize<List<Role>>(rolesData, options) ??
                throw new Exception("RolesData.json is empty");

            context.Roles?.AddRange(rolesList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the database with the users in the json file, then save changes in the database.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="options">Options to deserialize json</param>
        private static void SeedUsers(DataContext context, JsonSerializerOptions options)
        {
            var result = context.Users?.Any();
            if (result is true or null) return;

            var path = "src/Data/Seeders/UsersData.json";
            var usersData = File.ReadAllText(path);
            var usersList = JsonSerializer.Deserialize<List<User>>(usersData, options) ??
                throw new Exception("UsersData.json is empty");

            foreach (var user in usersList)
            {
                user.Id = Guid.NewGuid().ToString();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = hashedPassword;
                var daysCreatedRange = new Random().Next(-100, 0);
                user.CreatedAt = DateTime.UtcNow.AddDays(daysCreatedRange);
                var daysDeletedRange = new Random().Next(daysCreatedRange, 365);
                var deletedChance = new Random().Next(1, 10);
                if (deletedChance <= 2)
                    user.DeletedAt = DateTime.UtcNow.AddDays(daysDeletedRange);
                else
                    user.DeletedAt = null;
            }

            context.Users?.AddRange(usersList);
            context.SaveChanges();
        }
    }
}