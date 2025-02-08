using Domain.Contexts.Foods.Entities;
using Domain.Shared.Repositories;

namespace Domain.Contexts.Foods.Repositories.Contracts
{
  public interface IFoodRepository : IRepository<Food>
  {
    Task<List<Food>> GetByCodes(List<string> codes, CancellationToken cancellationToken);
    Task<Food?> GetProfile(Guid foodId, CancellationToken cancellationToken = default);
  }
}
