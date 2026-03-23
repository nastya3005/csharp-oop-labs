using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.Tree;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

public class CommandRegistry
{
    private readonly Dictionary<string, CommandMetadata> _commands;

    public CommandRegistry()
    {
        _commands = new Dictionary<string, CommandMetadata>(StringComparer.OrdinalIgnoreCase);
        InitializeCommands();
    }

    public bool TryGetMetadata(string commandName, out CommandMetadata? metadata)
    {
        return _commands.TryGetValue(commandName, out metadata);
    }

    private void InitializeCommands()
    {
        var connectMetadata = new CommandMetadata(typeof(ConnectCommand));
        connectMetadata.PositionalParameters.Add(new ParameterMetadata("Address", isRequired: false));
        connectMetadata.Flags["m"] = new FlagMetadata("m", isRequired: false, hasValue: true);
        _commands["connect"] = connectMetadata;

        _commands["disconnect"] = new CommandMetadata(typeof(DisconnectCommand));

        var treeGotoMetadata = new CommandMetadata(typeof(TreeGotoCommand));
        treeGotoMetadata.PositionalParameters.Add(new ParameterMetadata("Path", isRequired: false));
        _commands["tree goto"] = treeGotoMetadata;

        var treeListMetadata = new CommandMetadata(typeof(TreeListCommand));
        treeListMetadata.Flags["d"] = new FlagMetadata("d", isRequired: true, hasValue: true, defaultValue: "1");
        _commands["tree list"] = treeListMetadata;

        var fileShowMetadata = new CommandMetadata(typeof(FileShowCommand));
        fileShowMetadata.PositionalParameters.Add(new ParameterMetadata("Path", isRequired: false));
        fileShowMetadata.Flags["m"] = new FlagMetadata("m", isRequired: true, hasValue: true);
        _commands["file show"] = fileShowMetadata;

        var fileMoveMetadata = new CommandMetadata(typeof(FileMoveCommand));
        fileMoveMetadata.PositionalParameters.Add(new ParameterMetadata("SourcePath", isRequired: false));
        fileMoveMetadata.PositionalParameters.Add(new ParameterMetadata("DestinationPath", isRequired: false));
        _commands["file move"] = fileMoveMetadata;

        var fileCopyMetadata = new CommandMetadata(typeof(FileCopyCommand));
        fileCopyMetadata.PositionalParameters.Add(new ParameterMetadata("SourcePath", isRequired: false));
        fileCopyMetadata.PositionalParameters.Add(new ParameterMetadata("DestinationPath", isRequired: false));
        _commands["file copy"] = fileCopyMetadata;

        var fileDeleteMetadata = new CommandMetadata(typeof(FileDeleteCommand));
        fileDeleteMetadata.PositionalParameters.Add(new ParameterMetadata("Path", isRequired: false));
        _commands["file delete"] = fileDeleteMetadata;

        var fileRenameMetadata = new CommandMetadata(typeof(FileRenameCommand));
        fileRenameMetadata.PositionalParameters.Add(new ParameterMetadata("Path", isRequired: true));
        fileRenameMetadata.PositionalParameters.Add(new ParameterMetadata("Name", isRequired: true));
        _commands["file rename"] = fileRenameMetadata;
    }
}