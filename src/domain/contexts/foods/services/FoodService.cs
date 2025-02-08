using Domain.Contexts.Foods.Entities;
using Domain.Contexts.Foods.Services.Contracts;
using Domain.Services.Contracts;
using Quartz.Util;

namespace Domain.Contexts.Foods.Services
{
  public class FoodService : IFoodService
  {

    private IWrapperService _wrapperService;

    public FoodService(
      IWrapperService wrapperService
    )
    {
      _wrapperService = wrapperService;
    }
    public async Task<List<Component>> GenerateComponents(Guid foodId, string? foodCode)
    {
      if (foodCode.IsNullOrWhiteSpace())
      {
        return [];
      }
      var pageDocument = await _wrapperService.GetHtmlFromUrlAsync($"https://www.tbca.net.br/base-dados/int_composicao_estatistica.php?cod_produto={foodCode}");

      if (pageDocument is null)
      {
        return [];
      }

      var tableBody = _wrapperService.GetElement(pageDocument, "tbody");

      if (tableBody is null)
      {
        return [];
      }

      var rows = tableBody.ChildNodes;

      return rows.Select(r => new Component(r.ChildNodes[0].TextContent ?? "", r.ChildNodes[1].TextContent ?? "", r.ChildNodes[2].TextContent ?? "", foodId)).ToList();
    }
  }
}
