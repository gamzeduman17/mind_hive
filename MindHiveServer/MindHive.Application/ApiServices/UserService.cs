using System.Text;
using MindHive.Application.ApiServiceInterfaces;
using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
using MindHive.Application.DTOs.Dtos;
using MindHive.Domain.Entities;
using MindHive.Domain.Entities.Enums;
using MindHive.Domain.Repositories;

namespace MindHive.Application.ApiServices;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IErrorLogService _errorLogService;
    public UserService(IUserRepository userRepository, IJwtService jwtService,IErrorLogService errorLogService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _errorLogService = errorLogService;
    }

    public async Task<LoginResponseModel> Login(LoginRequestModel requestModel)
    {
        try
        {
            var user = await _userRepository.GetByUsernameAsync(requestModel.Username);

            if (user == null || user.PasswordHash != requestModel.Password)
            {
                return new LoginResponseModel
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            // ✅ JWT token üret
            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Role.ToString());

            return new LoginResponseModel
            {
                Success = true,
                Message = "Login successful",
                UserToken = token,
                User = new UserDto
                {
                    Username = user.Username,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                }
            };
        }
        catch (Exception ex)
        {
            await _errorLogService.LogAsync(nameof(UserService), ex.Message, ex.StackTrace);
            return new LoginResponseModel
            {
                Success = false,
                Message = "An unexpected error occurred. Please try again later."
            };
        }
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
        await _userRepository.SaveChangesAsync();

        // 4. Response hazırlama
        var responseData = new LoginResponseModel
        {
            Success = true,
            Message = " Register successful",
            UserToken = null,
            User = new UserDto
            {
                Username = newUser.Username,
                Role = newUser.Role,
                CreatedAt = newUser.CreatedAt,
                UpdatedAt = newUser.UpdatedAt,
                PasswordHash = newUser.PasswordHash,//not sure
            }
        };

        return BaseResponseModel<LoginResponseModel>.Ok(responseData, "User registered successfully");
    }

    private string HashPassword(string password)
    {
        // basit örnek, production'da daha güvenli hash kullan
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }

    public async Task<BaseResponseModel<UserDto>> UpdateProfile(Guid userId, UserUpdateRequestModel requestModel)
    {
        // 1️⃣ Kullanıcıyı bul
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return BaseResponseModel<UserDto>.Fail("User not found.");
        }

        // 2️⃣ Alanları güncelle
        user.Username = string.IsNullOrEmpty(requestModel.Username) ? user.Username : requestModel.Username;
        user.UserEmail = string.IsNullOrEmpty(requestModel.UserEmail) ? user.UserEmail : requestModel.UserEmail;
        user.Name = string.IsNullOrEmpty(requestModel.Name) ? user.Name : requestModel.Name;
        user.Surname = string.IsNullOrEmpty(requestModel.Surname) ? user.Surname : requestModel.Surname;
        user.UpdatedAt = DateTime.UtcNow;

        // 3️⃣ Şifre güncellenecekse
        if (!string.IsNullOrEmpty(requestModel.Password))
        {
            if (requestModel.Password != requestModel.ConfirmPassword)
                return BaseResponseModel<UserDto>.Fail("Passwords do not match.");

            // TODO: Hash kullanıyorsan burada hashle
            user.PasswordHash = requestModel.Password;
        }

        // 4️⃣ DB kaydı
        await _userRepository.UpdateAsync(user);

        // 5️⃣ Response dön
        return BaseResponseModel<UserDto>.Ok(new UserDto
        {
            Username = user.Username,
            UserEmail = user.UserEmail,
            Name = user.Name,
            Surname = user.Surname,
            UpdatedAt = user.UpdatedAt
        }, "Profile updated successfully.");
    }

}