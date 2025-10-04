using MindHive.Domain.Entities.Enums;

namespace MindHive.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public string UserEmail { get; set; } = string.Empty;
}