using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.DTOs.Auth;
using PerlaMetroUsersService.Services.Interfaces;

namespace PerlaMetroUsersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterPassengerRequestDto user, CancellationToken ct)
        {
            var createdUser = await _authService.Register(user, ct);
            return CreatedAtAction(actionName: nameof(UserController.GetUserById),
                                   controllerName: "Auth",
                                   routeValues: new { id = createdUser.Id },
                                   value: createdUser);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request, CancellationToken ct)
        {
            var token = await _authService.Login(request, ct);
            return Ok(token);
        }
    }
}
