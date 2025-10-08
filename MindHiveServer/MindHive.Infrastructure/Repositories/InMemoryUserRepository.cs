using MindHive.Domain.Entities;
using MindHive.Domain.Repositories;
using MindHive.Infrastructure;

namespace MindHive.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository, IRepository<User>
{
    private readonly List<User> _users = new List<User>();

    public User? GetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

    public User? GetByUsername(string username) => _users.FirstOrDefault(u => u.Username == username);

    public IEnumerable<User> GetAll() => _users;

    public IEnumerable<User> GetWhere(Func<User, bool> predicate) => _users.Where(predicate);

    public void Add(User user)
    {
        _users.Add(user);
    }

    public void AddRange(IEnumerable<User> entities)
    {
        _users.AddRange(entities);
    }

    public void Update(User user)
    {
        var existing = GetById(user.Id);
        if (existing != null)
        {
            existing.Username = user.Username;
            existing.PasswordHash = user.PasswordHash;
            existing.Role = user.Role;
            existing.UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Delete(User entity)
    {
        _users.Remove(entity);
    }

    public void DeleteById(Guid id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}
