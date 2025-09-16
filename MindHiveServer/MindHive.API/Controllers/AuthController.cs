using Microsoft.AspNetCore.Mvc;
using MindHive.Application.ApiServices;
using MindHive.Domain.Entities;

namespace MindHive.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (_userService.Login(request.Username, request.Password, out User? user))
        {
            return Ok(new { Message = "Login successful", Username = user.Username, Role = user.Role });
        }

        return Unauthorized(new { Message = "Invalid username or password" });
    }
}

public record LoginRequest(string Username, string Password);