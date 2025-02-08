namespace Shared.Commands.Contracts
{
  public interface ICommandResult
  {
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }
    public object? Errors { get; set; }
    public int? Status { get; set; }
  }
}