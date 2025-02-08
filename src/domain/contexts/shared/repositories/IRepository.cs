using System.Linq.Expressions;
using Shared.Entities;
namespace Domain.Shared.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
  Task<TEntity?> FindByIdAdync(Guid id, CancellationToken cancellationToken);
  Task<List<TEntity>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
  Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
  Task<List<TEntity>> ListWithPaginationAsync(string search = "", int skip = 0, int limit = int.MaxValue, CancellationToken cancellationToken = default);
  Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
  Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
  Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
  Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  Task DeleteAsync(Guid id, CancellationToken cancellationToken);
  Task<bool> IdExistsAsync(Guid id, CancellationToken cancellationToken);
  Task CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
  Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
}