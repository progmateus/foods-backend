using Domain.Contexts.Foods.Entities;
using Domain.Shared.Repositories;

namespace Domain.Contexts.Foods.Repositories.Contracts
{
  public interface IGroupRepository : IRepository<Group>
  {
    Task<List<Group>> GetByWrapperIdsAsync(List<string> wrapperIds, CancellationToken cancellationToken);
    Task<List<Group>> GetByNamesAsync(List<string> names, CancellationToken cancellationToken);
    bool HasAny();
  }
}
