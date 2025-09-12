using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Services;

public sealed class SystemClockService : IClockService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
