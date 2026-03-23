using Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Drivers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete;

public class ConnectCommand : FileSystemCommand
{
    public override string Name => "connect";

    public override string Description => "Connects the system to a specific file system on a given path";

    public override bool CanExecute(FileSystemSession session)
    {
        return !session.IsConnected;
    }

    protected override void ValidateParameters(IReadOnlyDictionary<string, object> parameters)
    {
        base.ValidateParameters(parameters);

        if (!parameters.ContainsKey("Address"))
        {
            throw new ArgumentException("Required parameter 'Address' not provided");
        }
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        string address = GetRequiredParameter<string>(parameters, "Address");
        string? mode = GetParameterValue(parameters, "Mode", "local");

        if (!PathResolver.IsAbsolute(address))
        {
            return CommandResult.Failure("Address must be an absolute path");
        }

        if (mode != null)
        {
            IFileSystemDriver driver = mode.ToLowerInvariant() switch
            {
                "local" => new LocalFileSystemDriver(),
                _ => throw new ArgumentException($"Unsupported file system mode: {mode}"),
            };

            try
            {
                session.Connect(address, driver);
                return CommandResult.Success($"Successfully connected to file system at '{address}'");
            }
            catch (Exception ex) when (
                ex is InvalidOperationException or
                    ArgumentException or
                    DirectoryNotFoundException)
            {
                return CommandResult.Failure($"Failed to connect: {ex.Message}");
            }
        }

        throw new InvalidOperationException();
    }
}