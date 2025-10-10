using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MindHive.Infrastructure.Data;

namespace MindHive.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly MindHiveDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(MindHiveDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await DeleteAsync(entity);
        }
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}