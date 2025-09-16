using MindHive.Domain.Entities;
using MindHive.Domain.Entities.Enums;
using MindHive.Domain.Repositories;

namespace MindHive.Application.ApiServices;

public class UserService
{
    private readonly IUserRepository _userRepository;

    // Constructor ile dependency injection
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        // Eğer repository boşsa test kullanıcı ekle
        if (_userRepository.GetAll() is { } users && !users.Any())
        {
            _userRepository.Add(new User
            {
                Username = "admin",
                PasswordHash = "1234", // Şimdilik plain text, daha sonra hash yapılacak
                Role = Role.Admin
            });
        }
    }

    public bool Login(string username, string password, out User? user)
    {
        user = _userRepository.GetByUsername(username);
        if (user != null && user.PasswordHash == password)
        {
            return true;
        }

        user = null;
        return false;
    }
}