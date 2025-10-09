using MindHive.Application.DTOs.CommonModels;
using MindHive.Application.DTOs.Dtos;
using MindHive.Domain.Entities.Enums;

namespace MindHive.Application.DTOs.Auth;

public class LoginResponseModel
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? UserToken { get; set; } // JWT token
    public UserDto? User { get; set; } 
}