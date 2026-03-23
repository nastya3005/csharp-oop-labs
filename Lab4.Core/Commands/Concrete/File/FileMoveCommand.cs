using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;

public class FileMoveCommand : FileSystemCommand
{
    public override string Name => "file move";

    public override string Description => "Moves a file from its source location to the specified location";

    public override bool CanExecute(FileSystemSession session)
    {
        return session.IsConnected;
    }

    protected override void ValidateParameters(IReadOnlyDictionary<string, object> parameters)
    {
        base.ValidateParameters(parameters);

        if (!parameters.ContainsKey("SourcePath"))
        {
            throw new ArgumentException("Required parameter 'SourcePath' not provided");
        }

        if (!parameters.ContainsKey("DestinationPath"))
        {
            throw new ArgumentException("Required parameter 'DestinationPath' not provided");
        }
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        string sourcePath = GetRequiredParameter<string>(parameters, "SourcePath");
        string destPath = GetRequiredParameter<string>(parameters, "DestinationPath");

        try
        {
            string resolvedSource = session.ResolvePath(sourcePath);
            string resolvedDest = session.ResolvePath(destPath);

            if (session.Driver == null)
            {
                return CommandResult.Failure("File system driver not available");
            }

            if (!session.Driver.Exists(resolvedSource))
            {
                return CommandResult.Failure($"Source file '{sourcePath}' does not exist");
            }

            if (session.Driver.IsDirectory(resolvedSource))
            {
                return CommandResult.Failure($"Source path '{sourcePath}' is a directory, not a file");
            }

            session.Driver.Move(resolvedSource, resolvedDest);

            return CommandResult.Success($"File '{sourcePath}' moved to '{destPath}'");
        }
        catch (Exception ex) when (
            ex is IOException or
            UnauthorizedAccessException)
        {
            return CommandResult.Failure($"Failed to move file: {ex.Message}");
        }
    }
}