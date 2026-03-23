namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Formatting;

public class TreeFormatOptions
{
    public string DirectorySymbol { get; set; } = "[D]";

    public string FileSymbol { get; set; } = "[F]";

    public string Indent { get; set; } = "  ";

    public int MaxDepth { get; set; } = 1;

    public bool ShowHidden { get; set; } = false;

    public bool ShowSize { get; set; } = false;

    public bool ShowDate { get; set; } = false;

    public TreeFormatOptions() { }

    public TreeFormatOptions(
        string directorySymbol = "[D]",
        string fileSymbol = "[F]",
        string indent = "  ",
        int maxDepth = 1)
    {
        DirectorySymbol = directorySymbol;
        FileSymbol = fileSymbol;
        Indent = indent;
        MaxDepth = maxDepth;
    }
}