namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

public interface IFileSystemNode
{
    string Name { get; }

    string FullPath { get; }

    bool IsDirectory { get; }

    long? Size { get; }

    DateTime? LastModified { get; }

    IFileSystemNode? Parent { get; }

    IEnumerable<IFileSystemNode> Children { get; }

    void Accept(IFileSystemNodeVisitor visitor);
}