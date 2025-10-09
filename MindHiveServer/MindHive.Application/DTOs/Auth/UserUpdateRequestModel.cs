namespace MindHive.Application.DTOs.Auth;

public class UserUpdateRequestModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string Name { get; set; }
    public string? Surname { get; set; } //not sure 
    public string ConfirmPassword { get; set; } = string.Empty;
}