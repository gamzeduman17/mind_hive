using MindHive.Application.ApiServiceInterfaces;
using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
using MindHive.Domain.Entities;
using MindHive.Domain.Entities.Enums;
using MindHive.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace MindHive.Application.ApiServices;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly string _jwtSecret;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _jwtSecret = configuration["Jwt:Secret"] ?? "your-super-secret-key-that-is-at-least-32-characters-long";
    }

    public async Task<(bool success, User? user)> Login(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user != null && user.PasswordHash == password) //will be fixed
            return (true, user);

        return (false, null);
    }

    public async Task<bool> UserExists(string username, string email)
    {
        var users = await _userRepository.GetWhereAsync(x => x.Username == username || x.UserEmail == email);
        return users.Any();
    }

    public async Task<BaseResponseModel<LoginResponseModel>> RegisterAsync(UserRegisterRequestModel request)
    {
        // 1. Şifre ve confirmPassword kontrolü
        if (request.Password != request.ConfirmPassword)
            return BaseResponseModel<LoginResponseModel>.Fail(
                "Passwords do not match",
                "PASSWORD_MISMATCH"
            );

        // 2. Kullanıcı var mı kontrol et
        if (await UserExists(request.Username, request.Email))
            return BaseResponseModel<LoginResponseModel>.Fail(
                "Username or email already exists",
                "USER_ALREADY_EXISTS"
            );

        // 3. Yeni kullanıcı oluştur
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            UserEmail = request.Email,
            PasswordHash = HashPassword(request.Password),
            Role = Role.User
        };

        await _userRepository.AddAsync(newUser);

        // 4. Response hazırlama
        var responseData = new LoginResponseModel
        {
            Username = newUser.Username,
            Role = newUser.Role
        };

        return BaseResponseModel<LoginResponseModel>.Ok(responseData, "User registered successfully");
    }

    private string HashPassword(string password)
    {
        // basit örnek, production'da daha güvenli hash kullan
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}