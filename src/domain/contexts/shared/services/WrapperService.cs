using AngleSharp;
using AngleSharp.Dom;
using Domain.Contexts.Foods.Repositories.Contracts;
using Domain.Services.Contracts;

namespace Http.Contexts.Shared.Services;

public class WrapperService : IWrapperService
{
  private readonly HttpClient _httpClient;


  public WrapperService()
  {
    _httpClient = new HttpClient();
  }

  // Método para obter o conteúdo HTML de uma URL e retornar um documento (IDocument)
  public async Task<IDocument> GetHtmlFromUrlAsync(string url)
  {
    try
    {
      // Usando GetStreamAsync para obter a resposta como um stream (fluxo binário)
      var responseStream = await _httpClient.GetStreamAsync(url);

      // Configura o contexto do AngleSharp e carrega o HTML diretamente do stream
      var config = Configuration.Default.WithDefaultLoader();
      var context = BrowsingContext.New(config);

      // Carrega o HTML a partir do stream
      var document = await context.OpenAsync(req => req.Content(responseStream));

      return document;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Erro ao buscar HTML: {ex.Message}");
      return null;
    }
  }

  // Método para obter o texto de um elemento a partir de uma URL e de um seletor CSS
  public async Task<string> GetElementTextFromUrlAsync(string url, string selector)
  {
    var document = await GetHtmlFromUrlAsync(url);
    if (document != null)
    {
      var element = document.QuerySelector(selector);
      return element?.TextContent ?? string.Empty;
    }
    return string.Empty;
  }

  // Método para obter os atributos de um elemento com base no seletor CSS e nome do atributo
  public IHtmlCollection<IElement> GetElements(IDocument document, string selector)
  {
    var elements = document.QuerySelectorAll(selector);

    return elements;
  }

  public IElement? GetElement(IDocument document, string selector)
  {
    var element = document.QuerySelector(selector);

    return element;
  }
}
