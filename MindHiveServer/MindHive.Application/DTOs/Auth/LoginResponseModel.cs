using MindHive.Application.DTOs.CommonModels;
using MindHive.Domain.Entities.Enums;

namespace MindHive.Application.DTOs.Auth;

public class LoginResponseModel
{
    public string Username { get; set; } = string.Empty;
    public Role Role { get; set; }
    public string UserToken { get; set; } = string.Empty;
}