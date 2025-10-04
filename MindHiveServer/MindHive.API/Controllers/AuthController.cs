using Microsoft.AspNetCore.Mvc;
using MindHive.Application.ApiServices;
using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
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
    public IActionResult Login([FromBody] LoginRequestModel request)
    {
        if (_userService.Login(request.Username, request.Password, out User? user))
        {
            var data = new LoginResponseModel
            {
                Username = user.Username,
                Role = user.Role
            };

            return Ok(BaseResponseModel<LoginResponseModel>.Ok(data, "Login successful"));
        }

        return Unauthorized(BaseResponseModel<LoginResponseModel>.Fail("Invalid username or password", "AUTH_INVALID"));
    }
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterRequestModel request)
    {
        
        var user = _userService.Register(request);

        var data = new LoginResponseModel
        {
            Username = user.Data.Username,
            Role = user.Data.Role
        };

        return Ok(BaseResponseModel<LoginResponseModel>.Ok(data, "Registration successful"));
    }

}