using MindHive.Application.DTOs.Auth;
using MindHive.Application.DTOs.CommonModels;
using MindHive.Application.DTOs.Dtos;
using MindHive.Domain.Entities;

namespace MindHive.Application.ApiServiceInterfaces;

public interface IUserService
{
    Task<LoginResponseModel> Login(LoginRequestModel requestModel);
    Task<bool> UserExists(string username, string email);
    Task<BaseResponseModel<LoginResponseModel>> RegisterAsync(UserRegisterRequestModel request);
    Task<BaseResponseModel<UserDto>> UpdateProfile(Guid userId, UserUpdateRequestModel requestModel);
}