namespace PerlaMetroUsersService.Services.Interfaces;

/// <summary>
/// Clock service interface for obtaining the current UTC time.
/// </summary>
public interface IClockService { DateTime UtcNow { get; } }
