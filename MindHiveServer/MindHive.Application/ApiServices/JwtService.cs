using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace MindHive.Application.ApiServices;

public class JwtService
{
    private readonly string _jwtSecret;
    private readonly int _jwtLifespanMinutes;
    private readonly HashSet<string> _blacklistedTokens = new();

    public JwtService(string secret, int lifespanMinutes = 60)
    {
        _jwtSecret = secret;
        _jwtLifespanMinutes = lifespanMinutes;
    }

    public string GenerateToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtLifespanMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool IsTokenValid(string token) => !_blacklistedTokens.Contains(token);

    public void BlacklistToken(string token) => _blacklistedTokens.Add(token);
}
