using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Domain.Common;

namespace Users.Infrastructure.Persistence;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    protected readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _context.Set<TEntity>();
    }

    public async virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
            _context.Remove(entity);
    }

    public virtual void DeleteAll(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        _context.RemoveRange(entities);
    }

    public async virtual Task<IEnumerable<TEntity>> ListAsync(int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async virtual Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public async virtual Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken: cancellationToken);
    }

    public async virtual Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.FindAsync<TEntity>([id], cancellationToken: cancellationToken);
    }

    public async virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await AddOrUpdateAsync(entity, cancellationToken, true);
    }

    public async virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await AddOrUpdateAsync(entity, cancellationToken);
    }

    public async virtual Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _context.AddRangeAsync(entities, cancellationToken);
    }

    public async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken, bool update = false)
    {
        var entry = _context.Entry(entity);

        if (entry.State == EntityState.Detached && update)
        {
            _context.Attach(entity);
            entry.State = EntityState.Modified;
        }

        switch (entry.State)
        {
            case EntityState.Detached:
                await _context.AddAsync(entity, cancellationToken);
                break;
            case EntityState.Modified:
                _context.Update(entity);
                break;
            case EntityState.Added:
                await _context.AddAsync(entity, cancellationToken);
                break;
            case EntityState.Unchanged:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
