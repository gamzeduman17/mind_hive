using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using MindHive.Application.ApiServiceInterfaces;

namespace MindHive.Application.ApiServices;

public class JwtService : IJwtService
{
    private readonly string _jwtSecret;
    private readonly int _jwtLifespanMinutes;
    private readonly HashSet<string> _blacklistedTokens = new();

    public JwtService(IConfiguration configuration)
    {
        _jwtSecret = configuration["Jwt:Secret"] ?? "your-super-secret-key-that-is-at-least-32-characters-long";
        _jwtLifespanMinutes = int.Parse(configuration["Jwt:LifespanMinutes"] ?? "60");
    }

    public string GenerateToken(Guid userId, string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // 🔹 userId
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtLifespanMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public bool IsTokenValid(string token) => !_blacklistedTokens.Contains(token);

    public void BlacklistToken(string token) => _blacklistedTokens.Add(token);
}