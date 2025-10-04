using MindHive.Domain.Entities;
using MindHive.Infrastructure;

namespace MindHive.Domain.Repositories;

public interface IUserRepository:IRepository<User>
{
    User? GetByUsername(string username);
}