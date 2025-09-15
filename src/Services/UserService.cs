using PerlaMetroUsersService.Services.Interfaces;
using PerlaMetroUsersService.Repositories.Interfaces;
using PerlaMetroUsersService.Mappers.Users;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Exceptions;

namespace PerlaMetroUsersService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IClockService _clock;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasherService passwordHasher, IClockService clock)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _clock = clock;
        }
        public async Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct = default)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(user.Email.Trim(), ct))
                throw new DuplicateException("User with this email already exists.");

            var roleName = user.Role?.Trim().ToLowerInvariant() ?? throw new ArgumentException("Role is required.");
            var role = await _unitOfWork.Roles.GetByNameAsync(roleName, ct) ??
                throw new NotFoundException("Role does not exist.");

            var newUser = UsersWriteMappers.CreateUser(user, role.Id, _passwordHasher, _clock);

            _unitOfWork.Users.Create(newUser);
            await _unitOfWork.SaveChangesAsync(ct);

            return await _unitOfWork.Users.GetByIdAsync(newUser.Id, UsersReadMappers.ToDetail, ct)
                   ?? throw new NotFoundException("User not found.");
        }
        public async Task Update(string id, EditUserRequestDto user, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var userId))
                throw new NotFoundException("User not found.");

            var existingUser = await _unitOfWork.Users.GetEntityByIdAsync(userId, ct) ??
                throw new NotFoundException("User not found.");
            UsersWriteMappers.ApplyProfileEdit(user, existingUser, _passwordHasher);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task Delete(string id, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var userId))
                throw new NotFoundException("User not found.");

            var existingUser = await _unitOfWork.Users.GetEntityByIdAsync(userId, ct) ??
                throw new NotFoundException("User not found.");
            _unitOfWork.Users.Delete(existingUser);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task SoftDelete(string id, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var userId))
                throw new NotFoundException("User not found.");

            var existingUser = await _unitOfWork.Users.GetEntityByIdAsync(userId, ct) ??
                throw new NotFoundException("User not found.");
            if (existingUser.DeletedAt != null)
                throw new ConflictException("User is already deleted.");
            existingUser.DeletedAt = _clock.UtcNow;
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct = default)
        {
            var normalizedStatus = string.IsNullOrWhiteSpace(status)
                ? "active"
                : status!.Trim().ToLowerInvariant();

            return await _unitOfWork.Users.GetAllAsync(
                UsersReadMappers.ToListItem,
                name,
                email,
                normalizedStatus,
                ct);
        }

        public async Task<GetUserResponseDto?> GetById(string id, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var userId))
                throw new NotFoundException("User not found.");

            return await _unitOfWork.Users.GetByIdAsync(userId, UsersReadMappers.ToDetail, ct)
                   ?? throw new NotFoundException("User not found.");
        }

    }
}
