using MindHive.Domain.Entities;
using MindHive.Infrastructure;

namespace MindHive.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
     Task<User?> GetByUsernameAsync(string username);
}