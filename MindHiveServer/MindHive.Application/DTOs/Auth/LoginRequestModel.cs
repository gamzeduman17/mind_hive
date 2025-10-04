namespace MindHive.Application.DTOs.Auth;

public class LoginRequestModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}