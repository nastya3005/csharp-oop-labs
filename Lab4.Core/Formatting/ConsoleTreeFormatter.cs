using Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Formatting;

public class ConsoleTreeFormatter : ITreeFormatter, IFileSystemNodeVisitor
{
    private readonly StringBuilder _output = new StringBuilder();
    private int _currentDepth;
    private TreeFormatOptions _options = new TreeFormatOptions();

    public string Format(IFileSystemNode root, TreeFormatOptions options)
    {
        _output.Clear();
        _currentDepth = 0;
        _options = options;

        root.Accept(this);
        return _output.ToString();
    }

    public void VisitFile(IFileSystemNode file)
    {
        if (_currentDepth > _options.MaxDepth)
            return;

        AppendIndent();
        _output.Append(_options.FileSymbol);
        _output.Append(' ');
        _output.Append(file.Name);

        if (_options.ShowSize && file.Size.HasValue)
        {
            _output.Append($" ({FormatSize(file.Size.Value)})");
        }

        if (_options.ShowDate && file.LastModified.HasValue)
        {
            _output.Append($" [{file.LastModified:yyyy-MM-dd HH:mm}]");
        }

        _output.AppendLine();
    }

    public void VisitDirectory(IFileSystemNode directory)
    {
        if (_currentDepth > _options.MaxDepth)
            return;

        AppendIndent();
        _output.Append(_options.DirectorySymbol);
        _output.Append(' ');
        _output.Append(directory.Name);

        if (_options.ShowDate && directory.LastModified.HasValue)
        {
            _output.Append($" [{directory.LastModified:yyyy-MM-dd HH:mm}]");
        }

        _output.AppendLine();

        if (_currentDepth < _options.MaxDepth)
        {
            _currentDepth++;
            foreach (IFileSystemNode child in directory.Children)
            {
                child.Accept(this);
            }

            _currentDepth--;
        }
    }

    private static string FormatSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }

        return $"{len:0.##} {sizes[order]}";
    }

    private void AppendIndent()
    {
        for (int i = 0; i < _currentDepth; i++)
        {
            _output.Append(_options.Indent);
        }
    }
}