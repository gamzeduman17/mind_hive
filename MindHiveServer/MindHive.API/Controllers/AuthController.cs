using System.Security.Claims;
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
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService):base(jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response = await _userService.Login(request);

        if (!response.Success)
            return Unauthorized(new { message = response?.Message ?? "Invalid username or password" });

        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
   
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

    [Authorize]
    [HttpPut("updateUserProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateRequestModel requestModel)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Invalid token" });

        var userId = Guid.Parse(userIdClaim);

        var response = await _userService.UpdateProfile(userId, requestModel);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}