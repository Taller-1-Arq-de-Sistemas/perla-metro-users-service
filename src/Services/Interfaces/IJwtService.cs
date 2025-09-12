namespace PerlaMetroUsersService.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(string email, string role);
    Task<(string email, string role)> ValidateToken(string token);
}