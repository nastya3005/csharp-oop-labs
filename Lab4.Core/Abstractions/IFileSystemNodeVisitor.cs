namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

public interface IFileSystemNodeVisitor
{
    void VisitFile(IFileSystemNode file);

    void VisitDirectory(IFileSystemNode directory);
}