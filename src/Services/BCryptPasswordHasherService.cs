using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Services;

public sealed class BCryptPasswordHasherService : IPasswordHasherService
{
    public string Hash(string plain) => BCrypt.Net.BCrypt.HashPassword(plain);
    public bool Verify(string plain, string hash) => BCrypt.Net.BCrypt.Verify(plain, hash);
}
