using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
using MindHive.Domain.Entities;

namespace MindHive.Application.ApiServiceInterfaces;

public interface IUserService
{
    Task<(bool success, User? user)> Login(string username, string password);
    Task<bool> UserExists(string username, string email);
    Task<BaseResponseModel<LoginResponseModel>> RegisterAsync(UserRegisterRequestModel request);
}