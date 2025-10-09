using MindHive.Domain.Entities.Enums;

namespace MindHive.Application.DTOs.Dtos;

public class UserDto
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public string UserEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Name { get; set; } = string.Empty;
    public string? Surname { get; set; } = string.Empty;
}