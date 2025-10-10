using MindHive.Domain.Entities;
using MindHive.Domain.Repositories;
using MindHive.Infrastructure.Data;

namespace MindHive.Infrastructure.Repositories;

public class ErrorLogRepository : BaseRepository<ErrorLog>, IErrorLogRepository
{
    public ErrorLogRepository(MindHiveDbContext context) : base(context)
    {
    }
}