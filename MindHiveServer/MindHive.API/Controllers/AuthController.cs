using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindHive.Application.ApiServices;
using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
using MindHive.Domain.Entities;

namespace MindHive.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly UserService _userService;

    public AuthController(UserService userService, JwtService jwtService)
        : base(jwtService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestModel request)
    {
        if (_userService.Login(request.Username, request.Password, out var user))
        {
            var token = _jwtService.GenerateToken(user!.Username, user.Role.ToString());
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
}