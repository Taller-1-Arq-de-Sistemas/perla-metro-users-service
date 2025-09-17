using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Controllers;

/// <summary>
/// Controller for authentication-related operations.
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Constructor for AuthController.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registers a new passenger and returns their details along with an authentication token.
    /// </summary>
    /// <param name="user">The registration details of the passenger.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The details of the registered passenger along with an authentication token.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterPassengerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterPassengerRequestDto user, CancellationToken ct)
    {
        var createdUser = await _authService.Register(user, ct);
        return CreatedAtAction(actionName: nameof(UserController.GetUserById),
                               controllerName: "User",
                               routeValues: new { id = createdUser.Id },
                               value: createdUser);
    }

    /// <summary>
    /// Logs in a user and returns an authentication token.
    /// </summary>
    /// <param name="request">The login request details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The login response containing user details and token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request, CancellationToken ct)
    {
        var response = await _authService.Login(request, ct);
        return Ok(response);
    }
}
