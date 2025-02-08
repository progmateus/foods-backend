using AngleSharp.Dom;

namespace Domain.Services.Contracts;

public interface IWrapperService
{
  Task<IDocument> GetHtmlFromUrlAsync(string url);

  Task<string> GetElementTextFromUrlAsync(string url, string selector);

  IHtmlCollection<IElement> GetElements(IDocument document, string selector);
  IElement? GetElement(IDocument document, string selector);
}
