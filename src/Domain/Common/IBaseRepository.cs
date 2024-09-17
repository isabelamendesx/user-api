namespace Users.Domain.Common;

public interface IBaseRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken, bool update = false);
    void DeleteAll(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> ListAsync(int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}
