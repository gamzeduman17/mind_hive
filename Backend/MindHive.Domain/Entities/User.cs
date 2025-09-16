public class User: : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.User;
}
public enum Role
{
    Admin,
    User,
    Guest
}
