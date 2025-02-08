using Domain.Contexts.Foods.Entities;
using Domain.Contexts.Foods.Repositories.Contracts;
using Domain.Contexts.Foods.Services.Contracts;
using Domain.Libs.MassTransit.Events;
using Domain.Services.Contracts;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.Extensions.Logging;

namespace ClassManager.Domain.Libs.MassTransit.Events;
public sealed class GenerateFoodsEventConsumer : IConsumer<GenerateFoodsEvent>
{
  private readonly IFoodRepository _foodRepository;
  private readonly IGroupRepository _groupRepository;
  private readonly IWrapperService _wrapperService;
  private readonly IFoodService _foodService;
  private readonly ILogger<GenerateFoodsEventConsumer> _logger;

  public GenerateFoodsEventConsumer(
    IFoodRepository foodRepository,
    IGroupRepository groupRepository,
    IWrapperService wrapperService,
    ILogger<GenerateFoodsEventConsumer> logger,
    IFoodService foodService

  )
  {
    _foodRepository = foodRepository;
    _groupRepository = groupRepository;
    _wrapperService = wrapperService;
    _logger = logger;
    _foodService = foodService;
  }
  public async Task Consume(ConsumeContext<GenerateFoodsEvent> context)
  {
    try
    {
      _logger.LogInformation("Event GenerateFoodsEventConsumer initialized");

      var pageDocument = await _wrapperService.GetHtmlFromUrlAsync(context.Message.pageUrl);

      if (pageDocument is null)
      {
        _logger.LogInformation("Event GenerateFoodsEventConsumer page not found");
        return;
      }

      var rows = _wrapperService.GetElements(pageDocument, "tr");

      var elementsCodes = rows.Select(linha => linha.ChildNodes[0].TextContent.Trim()).ToList();

      var existentFoods = await _foodRepository.GetByCodes(elementsCodes, new CancellationToken());

      var extistentFoodsCodes = existentFoods.Select(x => x.Code);

      var codesNotSaved = elementsCodes.Except(extistentFoodsCodes).ToList();

      var groupsNames = rows.Select(linha => linha.ChildNodes[3]?.TextContent?.Trim() ?? "").ToList();

      var groups = await _groupRepository.GetByNamesAsync(groupsNames, new CancellationToken());

      foreach (var linha in rows)
      {
        var cols = linha.ChildNodes;

        if (string.IsNullOrEmpty(cols[0].TextContent) || string.IsNullOrEmpty(cols[1].TextContent))
        {
          continue;
        }

        if (!codesNotSaved.Contains(cols[0].TextContent))
        {
          continue;
        }

        if (cols[0].TextContent == "Código")
        {
          continue;
        }

        var group = groups.FirstOrDefault(x => x.Name.ToLower() == cols[3]?.TextContent.ToLower().Trim());

        var food = new Food(cols[0].TextContent, cols[1].TextContent, cols[2]?.TextContent?.Trim() ?? "", group?.Id, cols[4]?.TextContent);

        var components = await _foodService.GenerateComponents(food.Id, food.Code);

        foreach (var component in components)
        {
          food.Components.Add(component);
        }

        await _foodRepository.CreateAsync(food, new CancellationToken());
      }

    }
    catch (Exception err)
    {
      _logger.LogInformation($"Event GenerateFoodsEventConsumer error: {err.Message}");
      throw new Exception(err.Message);
    }
  }
}