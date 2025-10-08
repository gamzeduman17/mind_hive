using System.Linq.Expressions;

namespace MindHive.Infrastructure;


public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();                                      
    Task<TEntity?> GetByIdAsync(Guid id);                                         
    Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate); 
    Task AddAsync(TEntity entity);                                                
    Task AddRangeAsync(IEnumerable<TEntity> entities);                            
    Task UpdateAsync(TEntity entity);                                             
    Task DeleteAsync(TEntity entity);                                             
    Task DeleteByIdAsync(Guid id);                                                
}