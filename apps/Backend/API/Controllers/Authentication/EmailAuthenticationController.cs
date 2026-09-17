using API.Models.Requests;
using API.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers.Authentication;

[ApiController]
[Route("auth/email")]
public class EmailAuthenticationController(EmailAuthService authService, TokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginByEmailRequest request)
    {
        if (!request.IsValidRequest())
            return BadRequest("Invalid login request.");

        var (isLogin, user) = await authService.Login(request.Email, request.Password);

        if (!isLogin || user is null) return Unauthorized();

        var tokens = tokenService.Create(user);
        return Ok(new { tokens });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterByEmailRequest request)
    {
        if (!request.IsValidRequest())
            return BadRequest("Invalid registration request.");

        try
        {
            var (wasCreated, _) = await authService.Register(request.Email, request.Password);

            if (wasCreated)
                Log.Information("New user registered: {Email}", request.Email);
            else
                Log.Information("Registration attempt for existing email: {Email}", request.Email);
            
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Registration handling failed by the email: {Email}", request.Email);
            return Problem();
        }
    }
}