using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Drivers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Formatting;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.Tree;

public class TreeListCommand : FileSystemCommand
{
    public override string Name => "tree list";

    public override string Description => "Output a slice of the file system relative to the current local path";

    public override bool CanExecute(FileSystemSession session)
    {
        return session.IsConnected;
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        string? depthString = GetParameterValue(parameters, "d", "1");
        if (depthString == null)
        {
            return CommandResult.Failure("Depth must be present");
        }

        int depth = int.Parse(depthString);
        if (depth < 1)
        {
            return CommandResult.Failure("Depth must be at least 1");
        }

        try
        {
            string currentPath = session.ResolvePath(".");

            if (session.Driver == null)
            {
                return CommandResult.Failure("File system driver not available");
            }

            var rootNode = new FileSystemNode(currentPath, session.Driver);

            var formatter = new ConsoleTreeFormatter();
            var options = new TreeFormatOptions
            {
                MaxDepth = depth,
                ShowSize = GetParameterValue(parameters, "ShowSize", false),
                ShowDate = GetParameterValue(parameters, "ShowDate", false),
            };

            string treeOutput = formatter.Format(rootNode, options);

            return CommandResult.Success(treeOutput);
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Failed to list directory: {ex.Message}");
        }
    }
}