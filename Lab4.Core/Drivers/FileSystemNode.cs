using Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Drivers;

public class FileSystemNode : IFileSystemNode
{
    private readonly IFileSystemDriver _driver;
    private IEnumerable<IFileSystemNode> _children;
    private bool _childrenLoaded;

    public string Name { get; }

    public string FullPath { get; }

    public bool IsDirectory { get; }

    public long? Size { get; }

    public DateTime? LastModified { get; }

    public IFileSystemNode? Parent { get; }

    public IEnumerable<IFileSystemNode> Children
    {
        get
        {
            if (!IsDirectory)
                return Enumerable.Empty<IFileSystemNode>();

            if (!_childrenLoaded)
            {
                LoadChildren();
                _childrenLoaded = true;
            }

            return _children;
        }
    }

    public FileSystemNode(
        string path,
        IFileSystemDriver driver,
        IFileSystemNode? parent = null)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        FullPath = path;
        Name = Path.GetFileName(path);
        Parent = parent;

        if (_driver.IsDirectory(path))
        {
            IsDirectory = true;
            Size = null;
            LastModified = _driver.GetDirectoryInfo(path)?.LastWriteTime;
        }
        else
        {
            IsDirectory = false;
            FileInfo fileInfo = _driver.GetFileInfo(path);
            Size = fileInfo.Length;
            LastModified = fileInfo.LastWriteTime;
        }

        _children = Enumerable.Empty<IFileSystemNode>();
    }

    public void Accept(IFileSystemNodeVisitor visitor)
    {
        if (IsDirectory)
        {
            visitor.VisitDirectory(this);

            foreach (IFileSystemNode child in Children)
            {
                child.Accept(visitor);
            }
        }
        else
        {
            visitor.VisitFile(this);
        }
    }

    public override string ToString()
    {
        return $"{Name} ({(IsDirectory ? "Directory" : "File")})";
    }

    private void LoadChildren()
    {
        if (!IsDirectory)
            return;

        var childNodes = new List<IFileSystemNode>();

        try
        {
            IEnumerable<string> childPaths = _driver.ListDirectory(FullPath);
            foreach (string childName in childPaths)
            {
                string childPath = Path.Combine(FullPath, childName);
                childNodes.Add(new FileSystemNode(childPath, _driver, this));
            }
        }
        catch (UnauthorizedAccessException)
        {
        }

        _children = childNodes;
    }
}