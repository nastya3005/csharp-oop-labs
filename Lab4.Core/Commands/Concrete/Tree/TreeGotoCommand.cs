using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.Tree;

public class TreeGotoCommand : FileSystemCommand
{
    public override string Name => "tree goto";

    public override string Description => "Navigates to the specified path relative to the connection point";

    public override bool CanExecute(FileSystemSession session)
    {
        return session.IsConnected;
    }

    protected override void ValidateParameters(IReadOnlyDictionary<string, object> parameters)
    {
        base.ValidateParameters(parameters);

        if (!parameters.ContainsKey("Path"))
        {
            throw new ArgumentException("Required parameter 'Path' not provided");
        }
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        string path = GetRequiredParameter<string>(parameters, "Path");

        try
        {
            session.ChangeDirectory(path);
            return CommandResult.Success($"Successfully changed directory to '{session.CurrentPath}'");
        }
        catch (Exception ex) when (
            ex is InvalidOperationException or
                DirectoryNotFoundException)
        {
            return CommandResult.Failure($"Failed to change directory: {ex.Message}");
        }
    }
}