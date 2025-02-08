using Shared.Commands.Contracts;

namespace Shared.Commands
{
  public class CommandResult : ICommandResult
  {

    public CommandResult(bool success, string message, object? data = null, object? errors = null, int status = 400)
    {
      IsSuccess = success;
      Message = message;
      Data = data;
      Errors = errors;
      Status = status;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }
    public object? Errors { get; set; }
    public int? Status { get; set; }
  }
}