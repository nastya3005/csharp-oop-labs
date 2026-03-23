using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.File;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Concrete.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class CommandParserTests
{
    private readonly CommandParser _parser;

    public CommandParserTests()
    {
        _parser = new CommandParser();
    }

    [Fact]
    public void Parse_ConnectCommand_ReturnsConnectCommandType()
    {
        string input = "connect /home/user";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<ConnectCommand>(result.Command);
    }

    [Fact]
    public void Parse_ConnectCommandWithFlag_ReturnsCommandWithParameters()
    {
        string input = "connect /home/user -m local";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<ConnectCommand>(result.Command);
        Assert.Equal("connect", result.Command.Name);
        Assert.Equal("/home/user", result.Parameters["Address"]);
        Assert.Equal("local", result.Parameters["m"]);
    }

    [Fact]
    public void Parse_ConnectCommandWithSpacesInPath_ReturnsCorrectCommand()
    {
        string input = "connect \"/home/my user/documents\" -m local";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<ConnectCommand>(result.Command);
        Assert.Equal("/home/my user/documents", result.Parameters["Address"]);
    }

    [Fact]
    public void Parse_DisconnectCommand_ReturnsDisconnectCommandType()
    {
        string input = "disconnect";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<DisconnectCommand>(result.Command);
        Assert.Equal("disconnect", result.Command.Name);
    }

    [Fact]
    public void Parse_TreeGotoCommand_ReturnsTreeGotoCommandType()
    {
        string input = "tree goto /home/user/docs";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<TreeGotoCommand>(result.Command);
        Assert.Equal("tree goto", result.Command.Name);
        Assert.Equal("/home/user/docs", result.Parameters["Path"]);
    }

    [Fact]
    public void Parse_TreeGotoCommandWithRelativePath_ReturnsCorrectCommand()
    {
        string input = "tree goto ../documents";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<TreeGotoCommand>(result.Command);
        Assert.Equal("../documents", result.Parameters["Path"]);
    }

    [Fact]
    public void Parse_TreeListCommand_ReturnsTreeListCommandType()
    {
        string input = "tree list -d 2";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<TreeListCommand>(result.Command);
        Assert.Equal("tree list", result.Command.Name);
        Assert.Equal("2", result.Parameters["d"]);
    }

    [Fact]
    public void Parse_TreeListCommandWithInvalidDepth_ReturnsCommandWithString()
    {
        string input = "tree list -d invalid";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<TreeListCommand>(result.Command);
        Assert.Equal("invalid", result.Parameters["d"]);
    }

    [Fact]
    public void Parse_FileShowCommand_ReturnsFileShowCommandType()
    {
        string input = "file show file.txt -m console";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileShowCommand>(result.Command);
        Assert.Equal("file show", result.Command.Name);
        Assert.Equal("file.txt", result.Parameters["Path"]);
        Assert.Equal("console", result.Parameters["m"]);
    }

    [Fact]
    public void Parse_FileShowCommandWithSpaces_ReturnsCorrectCommand()
    {
        string input = "file show \"my document.txt\" -m console";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileShowCommand>(result.Command);
        Assert.Equal("my document.txt", result.Parameters["Path"]);
    }

    [Fact]
    public void Parse_FileMoveCommand_ReturnsFileMoveCommandType()
    {
        string input = "file move source.txt destination/";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileMoveCommand>(result.Command);
        Assert.Equal("file move", result.Command.Name);
        Assert.Equal("source.txt", result.Parameters["SourcePath"]);
        Assert.Equal("destination/", result.Parameters["DestinationPath"]);
    }

    [Fact]
    public void Parse_FileMoveCommandWithAbsolutePaths_ReturnsCorrectCommand()
    {
        string input = "file move /home/user/source.txt /home/user/destination/";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileMoveCommand>(result.Command);
        Assert.Equal("/home/user/source.txt", result.Parameters["SourcePath"]);
        Assert.Equal("/home/user/destination/", result.Parameters["DestinationPath"]);
    }

    [Fact]
    public void Parse_FileCopyCommand_ReturnsFileCopyCommandType()
    {
        string input = "file copy source.txt backup/";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileCopyCommand>(result.Command);
        Assert.Equal("file copy", result.Command.Name);
        Assert.Equal("source.txt", result.Parameters["SourcePath"]);
        Assert.Equal("backup/", result.Parameters["DestinationPath"]);
    }

    [Fact]
    public void Parse_FileCopyCommandWithMixedPaths_ReturnsCorrectCommand()
    {
        string input = "file copy /absolute/source.txt relative/destination/";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileCopyCommand>(result.Command);
        Assert.Equal("/absolute/source.txt", result.Parameters["SourcePath"]);
        Assert.Equal("relative/destination/", result.Parameters["DestinationPath"]);
    }

    [Fact]
    public void Parse_FileDeleteCommand_ReturnsFileDeleteCommandType()
    {
        string input = "file delete file.txt";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileDeleteCommand>(result.Command);
        Assert.Equal("file delete", result.Command.Name);
        Assert.Equal("file.txt", result.Parameters["Path"]);
    }

    [Fact]
    public void Parse_FileDeleteCommandWithAbsolutePath_ReturnsCorrectCommand()
    {
        string input = "file delete /tmp/old.log";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileDeleteCommand>(result.Command);
        Assert.Equal("/tmp/old.log", result.Parameters["Path"]);
    }

    [Fact]
    public void Parse_FileRenameCommand_ReturnsFileRenameCommandType()
    {
        string input = "file rename old.txt new.txt";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileRenameCommand>(result.Command);
        Assert.Equal("file rename", result.Command.Name);
        Assert.Equal("old.txt", result.Parameters["Path"]);
        Assert.Equal("new.txt", result.Parameters["Name"]);
    }

    [Fact]
    public void Parse_FileRenameCommandWithAbsolutePath_ReturnsCorrectCommand()
    {
        string input = "file rename /home/user/old.doc document.docx";

        ParsingResult result = _parser.Parse(input);

        Assert.IsType<FileRenameCommand>(result.Command);
        Assert.Equal("/home/user/old.doc", result.Parameters["Path"]);
        Assert.Equal("document.docx", result.Parameters["Name"]);
    }
}