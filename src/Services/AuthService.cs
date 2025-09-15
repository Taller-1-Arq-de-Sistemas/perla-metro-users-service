using PerlaMetroUsersService.Services.Interfaces;
using PerlaMetroUsersService.Repositories.Interfaces;
using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.Exceptions;
using PerlaMetroUsersService.Mappers.Auth;
using PerlaMetroUsersService.Mappers.Users;
using System.Security.Authentication;

namespace PerlaMetroUsersService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClockService _clock;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IClockService clock, IPasswordHasherService passwordHasher, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _clock = clock;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct = default)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(user.Email, ct))
                throw new DuplicateException("User with this email already exists.");

            const string defaultRoleName = "passenger";
            var role = await _unitOfWork.Roles.GetByNameAsync(defaultRoleName, ct)
                       ?? throw new NotFoundException("Role does not exist.");

            var newUser = UsersWriteMappers.CreatePassenger(user, role.Id, _passwordHasher, _clock);
            _unitOfWork.Users.Create(newUser);
            await _unitOfWork.SaveChangesAsync(ct);

            var token = _jwtService.GenerateToken(newUser.Email, role.Name);
            var response = AuthResponseMappers.ToRegisterResponse(newUser, role.Name, token);

            return response;
        }

        public async Task<LoginUserResponseDto?> Login(LoginUserRequestDto user, CancellationToken ct = default)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(user.Email.Trim(), ct);
            if (existingUser == null || !_passwordHasher.Verify(user.Password, existingUser.Password) || existingUser.DeletedAt != null)
                throw new InvalidCredentialException("Invalid email or password.");

            var role = await _unitOfWork.Roles.GetByIdAsync(existingUser.RoleId, ct) ?? throw new NotFoundException("Role does not exist.");
            var token = _jwtService.GenerateToken(existingUser.Email, role.Name);
            var response = AuthResponseMappers.ToLoginResponse(token);

            return response;
        }
    }
}
