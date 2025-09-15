using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Creates a new user with the specified role.
        /// </summary>
        /// <returns>Created user profile.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto user, CancellationToken ct)
        {
            var createdUser = await _userService.Create(user, ct);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Returns a list of users.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ListUserResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers([FromQuery] ListUsersQueryDto query, CancellationToken ct)
        {
            var users = await _userService.GetAll(query.Name, query.Email, query.Status, ct);
            return Ok(users);
        }

        /// <summary>
        /// Returns the user by id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string id, CancellationToken ct)
        {
            var user = await _userService.GetById(id, ct);
            return Ok(user);
        }

        /// <summary>
        /// Updates the user profile fields.
        /// </summary>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUserRequestDto user, CancellationToken ct)
        {
            await _userService.Update(id, user, ct);
            return NoContent();
        }

        /// <summary>
        /// Soft-deletes the user.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(string id, CancellationToken ct)
        {
            await _userService.SoftDelete(id, ct);
            return NoContent();
        }
    }
}
