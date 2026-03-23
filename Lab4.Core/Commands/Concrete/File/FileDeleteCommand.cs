using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;

public class FileDeleteCommand : FileSystemCommand
{
    public override string Name => "file delete";

    public override string Description => "Removes the specified file from the FS.";

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
            string resolvedPath = session.ResolvePath(path);

            if (session.Driver == null)
            {
                return CommandResult.Failure("File system driver not available");
            }

            if (!session.Driver.Exists(resolvedPath))
            {
                return CommandResult.Failure($"File '{path}' does not exist");
            }

            if (session.Driver.IsDirectory(resolvedPath))
            {
                return CommandResult.Failure($"Path '{path}' is a directory, not a file");
            }

            session.Driver.Delete(resolvedPath);

            return CommandResult.Success($"File '{path}' deleted successfully");
        }
        catch (Exception ex) when (
            ex is IOException or
            UnauthorizedAccessException)
        {
            return CommandResult.Failure($"Failed to delete file: {ex.Message}");
        }
    }
}