using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;
using System.Diagnostics;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;

public class FileShowCommand : FileSystemCommand
{
    public override string Name => "file show";

    public override string Description => "Outputs the contents of a file in the specified mode";

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
        string? mode = GetParameterValue(parameters, "Mode", "console");

        try
        {
            string resolvedPath = session.ResolvePath(path);

            if (session.Driver == null)
            {
                return CommandResult.Failure("File system driver not available");
            }

            if (session.Driver.IsDirectory(resolvedPath))
            {
                return CommandResult.Failure($"Path '{path}' is a directory, not a file");
            }

            using Stream stream = session.Driver.OpenRead(resolvedPath);
            using var reader = new StreamReader(stream);
            string content = reader.ReadToEnd();

            Debug.Assert(mode != null, nameof(mode) + " != null");
            string output = mode.ToLowerInvariant() switch
            {
                "console" => FormatForConsole(content),
                _ => throw new ArgumentException($"Unsupported mode: {mode}"),
            };

            return CommandResult.Success(output);
        }
        catch (Exception ex) when (
            ex is FileNotFoundException or
            DirectoryNotFoundException or
            UnauthorizedAccessException)
        {
            return CommandResult.Failure($"Failed to show file: {ex.Message}");
        }
    }

    private static string FormatForConsole(string content)
    {
        return content;
    }
}