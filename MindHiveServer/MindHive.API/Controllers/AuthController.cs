using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindHive.Application.ApiServiceInterfaces;
using MindHive.Application.ApiServices;
using MindHive.Application.DTOs.Auth;

namespace MindHive.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IUserService _userService;

    public AuthController(UserService userService, JwtService jwtService)
        : base(jwtService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        var (success, user) = await _userService.Login(request.Username, request.Password);
        if (success && user != null)
        {
            var token = _jwtService.GenerateToken(user.Username, user.Role.ToString());
            return Ok(new { Message = "Login successful", Username = user.Username, Role = user.Role, Token = token });
        }

        return Unauthorized(new { Message = "Invalid username or password" });
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        var token = GetCurrentToken();
        if (token != null) _jwtService.BlacklistToken(token);

        return Ok(new { Message = "Logged out successfully" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel request)
    {
        var result = await _userService.RegisterAsync(request);
        
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}