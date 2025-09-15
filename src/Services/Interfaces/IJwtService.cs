namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// JWT service interface for token generation and validation.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates a JWT token for the given email and role.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="role">The role of the user.</param>
    /// <returns>The generated JWT token.</returns>
    string GenerateToken(string email, string role);
    /// <summary>
    /// Validates the given JWT token and returns the email and role if valid.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>A tuple containing the email and role if the token is valid, or an error if not.</returns>
    Task<(string email, string role)> ValidateToken(string token);
}
