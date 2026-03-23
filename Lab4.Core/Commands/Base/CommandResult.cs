namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;

public class CommandResult
{
    public bool IsSuccess { get; }

    public string Message { get; }

    public object? Data { get; }

    private CommandResult(bool isSuccess, string message, object? data = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static CommandResult Success(string message = "Command executed successfully", object? data = null)
        => new CommandResult(true, message, data);

    public static CommandResult Failure(string message, object? data = null)
        => new CommandResult(false, message, data);
}