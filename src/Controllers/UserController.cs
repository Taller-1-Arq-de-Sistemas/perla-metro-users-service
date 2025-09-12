using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto user, CancellationToken ct)
        {
            var createdUser = await _userService.Create(user, ct);
            return CreatedAtAction(actionName: nameof(GetUserById),
                                   controllerName: "User",
                                   routeValues: new { id = createdUser.Id },
                                   value: createdUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken ct)
        {
            var users = await _userService.GetAll(ct);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id, CancellationToken ct)
        {
            var user = await _userService.GetById(id, ct);
            return Ok(user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUserRequestDto user, CancellationToken ct)
        {
            await _userService.Update(id, user, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id, CancellationToken ct)
        {
            await _userService.SoftDelete(id, ct);
            return NoContent();
        }
    }
}