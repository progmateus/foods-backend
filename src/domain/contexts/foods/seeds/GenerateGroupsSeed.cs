
using Domain.Contexts.Foods.Entities;
using Domain.Contexts.Foods.Repositories.Contracts;
using Domain.Services.Contracts;

namespace Domain.Contexts.Seeds;

public class GenerateGroupSeed
{
  private IGroupRepository _groupRepository;
  private IWrapperService _wrapperService;

  public GenerateGroupSeed(
    IGroupRepository groupRepository,
    IWrapperService wrapperService
    )
  {
    _groupRepository = groupRepository;
    _wrapperService = wrapperService;
  }
  public async Task Execute()
  {

    if (_groupRepository.HasAny())
    {
      return;
    }

    var pageDocument = await _wrapperService.GetHtmlFromUrlAsync("https://www.tbca.net.br/base-dados/composicao_estatistica.php");
    var selectGroupElement = _wrapperService.GetElement(pageDocument, "#cmb_grupo");

    if (selectGroupElement is not null)
    {
      var options = selectGroupElement.Children;

      var groups = options.Where(x => !string.IsNullOrEmpty(x.GetAttribute("value")) || string.IsNullOrEmpty(x.TextContent)).Select(x => new Group(x.GetAttribute("value") ?? "", x.TextContent)).ToList();

      await _groupRepository.CreateRangeAsync(groups, new CancellationToken());
    }
  }
}