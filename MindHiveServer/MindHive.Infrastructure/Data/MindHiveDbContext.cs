using Microsoft.EntityFrameworkCore;
using MindHive.Domain.Entities;

namespace MindHive.Infrastructure.Data;

public class MindHiveDbContext : DbContext
{
    public MindHiveDbContext(DbContextOptions<MindHiveDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}