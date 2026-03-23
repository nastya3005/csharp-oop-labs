using Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Formatting;

public interface ITreeFormatter
{
    string Format(IFileSystemNode root, TreeFormatOptions options);
}