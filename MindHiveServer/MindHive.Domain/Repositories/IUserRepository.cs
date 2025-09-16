using MindHive.Domain.Entities;

namespace MindHive.Domain.Repositories;

public interface IUserRepository
{
    User? GetById(Guid id);
    User? GetByUsername(string username);
    IEnumerable<User> GetAll();
    void Add(User user);
    void Update(User user);
    void Delete(Guid id);
}