using PerlaMetroUsersService.Services.Interfaces;
using PerlaMetroUsersService.Repositories.Interfaces;
using PerlaMetroUsersService.Models;

namespace PerlaMetroUsersService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            string userId = Guid.NewGuid().ToString();
            user.Id = userId;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> UpdateUserAsync(string id, User user)
        {
            return await _userRepository.UpdateUserAsync(id, user);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}