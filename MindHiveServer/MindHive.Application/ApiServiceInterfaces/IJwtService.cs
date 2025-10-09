namespace MindHive.Application.ApiServiceInterfaces;

public interface IJwtService
{
    string GenerateToken(Guid userId, string username, string role);
    bool IsTokenValid(string token);
    void BlacklistToken(string token);
}