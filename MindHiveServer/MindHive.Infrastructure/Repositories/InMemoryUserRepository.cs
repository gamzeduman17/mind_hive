using MindHive.Domain.Entities;

namespace MindHive.Domain.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    public User? GetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

    public User? GetByUsername(string username) => _users.FirstOrDefault(u => u.Username == username);

    public IEnumerable<User> GetAll() => _users;

    public void Add(User user)
    {
        _users.Add(user);
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

    public void Delete(Guid id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}
