using Domain.Contexts.Foods.Repositories.Contracts;
using Domain.Libs.MassTransit.Publish;
using Domain.Services.Contracts;
using Shared.Commands;
using Shared.Commands.Contracts;

namespace Domain.Contexts.Foods.Handlers;

public class ListFoodsHandler
{
  private IFoodRepository _foodRepository;
  private IWrapperService _wrapperService;
  private IGroupRepository _groupRepository;
  private IPublishBus _publishBus;
  public ListFoodsHandler(
    IFoodRepository foodRepository,
    IWrapperService wrapperService,
    IGroupRepository groupRepository,
    IPublishBus publishBus
    )
  {
    _foodRepository = foodRepository;
    _wrapperService = wrapperService;
    _groupRepository = groupRepository;
    _publishBus = publishBus;
  }
  public async Task<ICommandResult> Handle(PaginationCommand command)
  {
    if (command.Page < 1) command.Page = 1;

    var skip = (command.Page - 1) * command.Limit;

    var foods = await _foodRepository.ListWithPaginationAsync(command.Search, skip, 20, new CancellationToken());

    return new CommandResult(true, "FOODS_LISTED", foods, null, 200);
  }
}
