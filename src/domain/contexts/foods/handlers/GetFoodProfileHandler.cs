using Domain.Contexts.Foods.Repositories.Contracts;
using Shared.Commands;
using Shared.Commands.Contracts;

namespace Domain.Contexts.Foods.Handlers;

public class GetFoodProfileHandler
{
  private IFoodRepository _foodRepository;
  public GetFoodProfileHandler(
    IFoodRepository foodRepository
    )
  {
    _foodRepository = foodRepository;
  }
  public async Task<ICommandResult> Handle(Guid id)
  {
    var profile = await _foodRepository.GetProfile(id, new CancellationToken());

    if (profile is null)
    {
      return new CommandResult(false, "ERR_FOOD_NOT_FOUND", new { }, null, 404);
    }

    return new CommandResult(true, "FOOD_GOTTEN", profile, null, 200);
  }
}
