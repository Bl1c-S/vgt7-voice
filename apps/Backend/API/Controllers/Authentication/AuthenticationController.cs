using API.Models.Requests;
using API.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Authentication;

[ApiController]
[Route("auth/")]
public class AuthenticationController(AuthService authService, TokenService tokenService) : ControllerBase
{
    [HttpPost("login/email/")]
    public IActionResult Login([FromBody] LoginByEmailRequest request)
    {
        if (!request.IsValidRequest())
            return BadRequest("Invalid login request.");

        var isLogin = authService.Login(request.Email, request.Password, out var user);
        if (!isLogin || user is null) return Forbid();

        var tokens = tokenService.Create(user);
        return Ok(new { tokens });
    }

    [HttpPost("register/email/")]
    public IActionResult Register([FromBody] RegisterByEmailRequest request)
    {
        if (!request.IsValidRequest())
            return BadRequest("Invalid registration request.");

        try
        {
            var isRegister = authService.Register(request.Email, request.Password, out var user);
            if (!isRegister || user != null) return Conflict("Email already exists.");

            var tokens = tokenService.Create(user!);
            return Ok(new { tokens });
        }
        catch
        {
            return Problem(detail: "Internal server registration error.",
                statusCode: StatusCodes.Status500InternalServerError, title: "Internal Server Error");
        }
    }
}