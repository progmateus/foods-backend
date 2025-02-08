using Data.Contexts.shared.Repositories;
using Data.Database;
using Domain.Contexts.Foods.Entities;
using Domain.Contexts.Foods.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts.Foods.Repositories;

public class FoodRepository : Repository<Food>, IFoodRepository
{
  public FoodRepository(AppDbContext context) : base(context) { }

  public async Task<List<Food>> GetByCodes(List<string> codes, CancellationToken cancellationToken)
  {
    return await DbSet.Where(x => codes.Contains(x.Code)).ToListAsync();
  }

  public async Task<Food?> GetProfile(Guid foodId, CancellationToken cancellationToken = default)
  {
    return await DbSet.Include(x => x.Components).Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == foodId);
  }

  public async Task<List<Food>> ListWithPaginationAsync(string search = "", int skip = 0, int limit = int.MaxValue, CancellationToken cancellationToken = default)
  {
    return await DbSet.Where(x => string.IsNullOrEmpty(search) || x.Name.Contains(search)).Skip(skip).Take(limit).ToListAsync(cancellationToken);
  }
}
