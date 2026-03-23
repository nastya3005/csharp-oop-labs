using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;

public class FileCopyCommand : FileSystemCommand
{
    public override string Name => "file copy";

    public override string Description => "Copies file from source location to destination directory";

    public override bool CanExecute(FileSystemSession session)
    {
        return session.IsConnected;
    }

    protected override void ValidateParameters(IReadOnlyDictionary<string, object> parameters)
    {
        base.ValidateParameters(parameters);

        if (parameters.Count < 2)
        {
            throw new ArgumentException("File copy requires 2 parameters: source and destination");
        }
    }

    protected override CommandResult ExecuteInternal(
        FileSystemSession session,
        IReadOnlyDictionary<string, object> parameters)
    {
        var paramList = parameters.Values.ToList();

        if (paramList.Count < 2 ||
            paramList[0] is not string sourcePath ||
            paramList[1] is not string destPath)
        {
            return CommandResult.Failure("Invalid parameters for file copy");
        }

        try
        {
            string resolvedSource = session.ResolvePath(sourcePath);
            string resolvedDest = session.ResolvePath(destPath);

            if (session.Driver == null)
                return CommandResult.Failure("File system driver not available");

            if (!session.Driver.Exists(resolvedSource))
                return CommandResult.Failure($"Source file '{sourcePath}' does not exist");

            if (session.Driver.IsDirectory(resolvedSource))
                return CommandResult.Failure($"'{sourcePath}' is a directory, not a file");

            if (!session.Driver.IsDirectory(resolvedDest))
            {
                return CommandResult.Failure(
                    $"Destination must be a directory: '{destPath}'",
                    new
                    {
                        Source = resolvedSource,
                        Destination = resolvedDest,
                        IsDirectory = session.Driver.IsDirectory(resolvedDest),
                    });
            }

            if (!session.Driver.Exists(resolvedDest))
            {
                return CommandResult.Failure($"Destination directory does not exist: '{destPath}'");
            }

            string fileName = Path.GetFileName(resolvedSource);
            string fullDestPath = Path.Combine(resolvedDest, fileName);

            fullDestPath = session.Driver.GetFullPath(fullDestPath);

            if (session.Driver.Exists(fullDestPath))
            {
                return CommandResult.Failure(
                    $"Cannot copy file: '{fileName}' already exists in destination directory",
                    new
                    {
                        SourceFile = fileName,
                        SourcePath = resolvedSource,
                        DestinationDirectory = resolvedDest,
                        ConflictingPath = fullDestPath,
                    });
            }

            session.Driver.Copy(resolvedSource, fullDestPath);

            return CommandResult.Success(
                $"File '{Path.GetFileName(sourcePath)}' copied to directory '{destPath}'",
                new
                {
                    Source = resolvedSource,
                    Destination = fullDestPath,
                    SourceFileName = Path.GetFileName(resolvedSource),
                    DestinationDirectory = resolvedDest,
                });
        }
        catch (Exception ex) when (
            ex is IOException or
            UnauthorizedAccessException or
            ArgumentException)
        {
            return CommandResult.Failure($"Failed to copy file: {ex.Message}");
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Unexpected error during file copy: {ex.Message}");
        }
    }
}