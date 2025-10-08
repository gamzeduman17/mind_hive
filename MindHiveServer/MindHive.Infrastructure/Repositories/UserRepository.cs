using Microsoft.EntityFrameworkCore;
using MindHive.Domain.Entities;
using MindHive.Domain.Repositories;
using MindHive.Infrastructure.Data;

namespace MindHive.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MindHiveDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}