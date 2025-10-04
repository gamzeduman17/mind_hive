using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
using MindHive.Domain.Entities;
using MindHive.Domain.Entities.Enums;
using MindHive.Domain.Repositories;

namespace MindHive.Application.ApiServices;

public class UserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        // Eğer repository boşsa test kullanıcı ekle
        if (_userRepository.GetAll() is { } users && !users.Any())
        {
            _userRepository.Add(new User
            {
                Username = "admin",
                PasswordHash = "1234", // hashhh
                Role = Role.Admin
            });
        }
    }

    public bool Login(string username, string password, out User? user)
    {
        user = _userRepository.GetByUsername(username);
        if (user != null && user.PasswordHash == password) //will be fixed
        {
            return true;
        }

        user = null;
        return false;
    }

    public bool UserExists(string username, string email)
    {
        var users = _userRepository.GetWhere(x=>x.Username==username || x.UserEmail==email);
        return users.Any();
    }
    public BaseResponseModel<LoginResponseModel> Register(UserRegisterRequestModel request)
    {
        // 1. Şifre ve confirmPassword kontrolü
        if(request.Password != request.ConfirmPassword)
        {
            return BaseResponseModel<LoginResponseModel>.Fail(
                "Passwords do not match",
                "PASSWORD_MISMATCH"
            );
        }

        // 2. Kullanıcı var mı kontrol et
        if (UserExists(request.Username, request.Email))
        {
            return BaseResponseModel<LoginResponseModel>.Fail(
                "Username or email already exists",
                "USER_ALREADY_EXISTS"
            );
        }

        // 3. Yeni kullanıcı oluştur
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            UserEmail = request.Email,
            PasswordHash = HashPassword(request.Password),
            Role = Role.User
        };

        _userRepository.Add(newUser);

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