
using Data.Contexts.shared.Repositories;
using Data.Database;
using Domain.Contexts.Foods.Entities;
using Domain.Contexts.Foods.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts.Foods.Repositories;

public class GroupRepository : Repository<Group>, IGroupRepository
{
  public GroupRepository(AppDbContext context) : base(context) { }

  public async Task<List<Group>> GetByNamesAsync(List<string> names, CancellationToken cancellationToken)
  {
    return await DbSet.Where(x => names.Contains(x.Name)).ToListAsync();
  }

  public async Task<List<Group>> GetByWrapperIdsAsync(List<string> wrapperIds, CancellationToken cancellationToken)
  {
    return await DbSet.Where(x => wrapperIds.Contains(x.WrapperId)).ToListAsync();
  }

  public bool HasAny()
  {
    return DbSet.Any();
  }
}
