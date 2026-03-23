using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;

public abstract class FileSystemCommand : ICommand
{
    public abstract string Name { get; }

    public abstract string Description { get; }

    public CommandResult Execute(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        try
        {
            ValidateParameters(parameters);
            return ExecuteInternal(session, parameters);
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Error executing command '{Name}': {ex.Message}");
        }
    }

    public virtual bool CanExecute(FileSystemSession session)
    {
        return true;
    }

    protected virtual void ValidateParameters(IReadOnlyDictionary<string, object> parameters)
    {
    }

    protected abstract CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters);

    protected T? GetParameterValue<T>(IReadOnlyDictionary<string, object> parameters, string name, T? defaultValue = default)
    {
        if (parameters.TryGetValue(name, out object? value))
        {
            return (T?)value;
        }

        return defaultValue;
    }

    protected T GetRequiredParameter<T>(IReadOnlyDictionary<string, object> parameters, string name)
    {
        if (!parameters.TryGetValue(name, out object? value))
        {
            throw new ArgumentException($"Required parameter '{name}' not provided");
        }

        if (value is T typedValue)
        {
            return typedValue;
        }

        throw new ArgumentException($"Parameter '{name}' has incorrect type");
    }
}