namespace PerlaMetroUsersService.Services.Interfaces;

public interface IPasswordHasherService
{
    string Hash(string plain);
    bool Verify(string plain, string hash);
}
