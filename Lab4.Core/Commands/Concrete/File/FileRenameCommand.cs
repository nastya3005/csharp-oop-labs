using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;

public class FileRenameCommand : FileSystemCommand
{
    public override string Name => "file rename";

    public override string Description => "Renames the file at the specified path to the specified name";

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

        if (!parameters.ContainsKey("Name"))
        {
            throw new ArgumentException("Required parameter 'Name' not provided");
        }

        string newName = GetRequiredParameter<string>(parameters, "Name");
        if (newName.Contains("/", StringComparison.Ordinal) ||
            newName.Contains("\\", StringComparison.Ordinal) ||
            newName.Contains(":", StringComparison.Ordinal))
        {
            throw new ArgumentException("New name cannot contain path separators");
        }
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        string path = GetRequiredParameter<string>(parameters, "Path");
        string newName = GetRequiredParameter<string>(parameters, "Name");

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

            string directory = Path.GetDirectoryName(resolvedPath) ??
                throw new ArgumentException("Invalid file path");

            string newPath = Path.Combine(directory, newName);

            if (session.Driver.Exists(newPath))
            {
                return CommandResult.Failure($"File with name '{newName}' already exists in directory");
            }

            session.Driver.Move(resolvedPath, newPath);

            return CommandResult.Success($"File '{path}' renamed to '{newName}'");
        }
        catch (Exception ex) when (
            ex is IOException or
            UnauthorizedAccessException)
        {
            return CommandResult.Failure($"Failed to rename file: {ex.Message}");
        }
    }
}