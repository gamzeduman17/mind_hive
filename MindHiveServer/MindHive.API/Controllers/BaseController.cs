using Microsoft.AspNetCore.Mvc;
using MindHive.Application.ApiServices;

namespace MindHive.API.Controllers;

public class BaseController: ControllerBase
{
    protected readonly JwtService _jwtService;

    public BaseController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    protected string? GetCurrentToken()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authHeader)) return null;

        if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return null;

        return authHeader.Substring("Bearer ".Length).Trim();
    }


    protected bool ValidateCurrentToken()
    {
        var token = GetCurrentToken();
        if (token == null) return false;
        return _jwtService.IsTokenValid(token);
    }
}