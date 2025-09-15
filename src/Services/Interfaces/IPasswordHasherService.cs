namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// Password hasher service interface for hashing and verifying passwords.
/// </summary>
public interface IPasswordHasherService
{
    /// <summary>
    /// Hashes the given plain text password.
    /// </summary>
    /// <param name="plain">The plain text password to hash.</param>
    /// <returns>The hashed password.</returns>
    string Hash(string plain);
    /// <summary>
    /// Verifies the given plain text password against the hashed password.
    /// </summary>
    /// <param name="plain">The plain text password to verify.</param>
    /// <param name="hash">The hashed password to compare against.</param>
    /// <returns>True if the passwords match, false otherwise.</returns>
    bool Verify(string plain, string hash);
}
