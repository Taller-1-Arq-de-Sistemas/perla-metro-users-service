using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.DTOs.Users;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Controllers;

/// <summary>
/// Controller for user-related operations.
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor for UsersController.
    /// </summary>
    /// <param name="userService">The user service.</param>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user creation details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The created user profile.</returns>
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
    /// Returns all users, with optional filtering by name, email, and status.
    /// </summary>
    /// <param name="query">The query parameters for filtering users.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A list of users matching the query.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ListUserResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers([FromQuery] ListUsersQueryDto query, CancellationToken ct)
    {
        var users = await _userService.GetAll(query.Name, query.Email, query.Status, ct);
        return Ok(users);
    }

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The user profile if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(string id, CancellationToken ct)
    {
        var user = await _userService.GetById(id, ct);
        return Ok(user);
    }

    /// <summary>
    /// Updates an existing user's details.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="user">The updated user details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A 204 No Content response if the update was successful; otherwise, an error response.</returns>
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
    /// Soft deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A 204 No Content response if the deletion was successful; otherwise, an error response.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(string id, CancellationToken ct)
    {
        await _userService.SoftDelete(id, ct);
        return NoContent();
    }
}
