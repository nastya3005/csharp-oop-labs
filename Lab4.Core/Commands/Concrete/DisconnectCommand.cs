using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete;

public class DisconnectCommand : FileSystemCommand
{
    public override string Name => "disconnect";

    public override string Description => "Disconnects from the file system";

    public override bool CanExecute(FileSystemSession session)
    {
        return session.IsConnected;
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        try
        {
            session.Disconnect();
            return CommandResult.Success("Successfully disconnected from file system");
        }
        catch (InvalidOperationException ex)
        {
            return CommandResult.Failure(ex.Message);
        }
    }
}