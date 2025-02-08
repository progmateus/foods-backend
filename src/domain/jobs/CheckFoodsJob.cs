
using Domain.Libs.MassTransit.Events;
using Domain.Libs.MassTransit.Publish;
using Domain.Services.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Domain.Jobs;

[DisallowConcurrentExecution]
public class CheckFoodsJob : IJob
{
  private readonly ILogger<CheckFoodsJob> _logger;
  private readonly IWrapperService _wrapperService;
  private IPublishBus _publishBus;
  public CheckFoodsJob(
    ILogger<CheckFoodsJob> logger,
    IWrapperService wrapperService,
    IPublishBus publishBus
  )
  {
    _logger = logger;
    _wrapperService = wrapperService;
    _publishBus = publishBus;
  }
  public async Task Execute(IJobExecutionContext context)
  {
    try
    {
      _logger.LogInformation("Job CheckFoodsJob initialized");
      int page = 1;
      var hasMore = true;
      while (hasMore)
      {
        var url = $"https://www.tbca.net.br/base-dados/composicao_estatistica.php?pagina={page}";
        var pageDocument = await _wrapperService.GetHtmlFromUrlAsync(url);

        var linhas = _wrapperService.GetElements(pageDocument, "tr").ToList();

        if (linhas.Count == 1)
        {
          hasMore = false;
          break;
        }
        var eventRequest = new GenerateFoodsEvent(url);
        await _publishBus.PublicAsync(eventRequest);
        page++;
      }
    }
    catch (Exception err)
    {
      _logger.LogInformation($"Job CheckFoodsJob error: {err.Message}");
      throw new Exception(err.Message);
    }
  }
}