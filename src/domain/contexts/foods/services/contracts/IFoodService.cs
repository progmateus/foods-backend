using Domain.Contexts.Foods.Entities;

namespace Domain.Contexts.Foods.Services.Contracts
{
  public interface IFoodService
  {
    Task<List<Component>> GenerateComponents(Guid foodId, string foodCode);
  }
}
