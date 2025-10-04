namespace MindHive.Infrastructure;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();                      
    TEntity? GetById(Guid id);                           
    IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate); 
    void Add(TEntity entity);                            
    void AddRange(IEnumerable<TEntity> entities);         
    void Update(TEntity entity);                        
    void Delete(TEntity entity);                          
    void DeleteById(Guid id);                             
}
