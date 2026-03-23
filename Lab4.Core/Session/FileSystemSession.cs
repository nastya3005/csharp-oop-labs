using Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

public class FileSystemSession
{
    private static string CombinePaths(string path1, string path2)
    {
        if (path1 == "/")
            return "/" + path2.TrimStart('/');

        return path1.TrimEnd('/') + "/" + path2.TrimStart('/');
    }

    public string? CurrentPath { get; private set; }

    public string? RootPath { get; private set; }

    public bool IsConnected => Driver != null;

    public void Connect(string rootPath, IFileSystemDriver driver)
    {
        if (IsConnected)
            throw new InvalidOperationException("Already connected to a file system");

        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        ArgumentNullException.ThrowIfNull(driver);

        if (!driver.Exists(rootPath))
            throw new DirectoryNotFoundException($"Path '{rootPath}' does not exist");

        Driver = driver;
        RootPath = rootPath;
        CurrentPath = "/";
    }

    public void Disconnect()
    {
        if (!IsConnected)
            throw new InvalidOperationException("Not connected to any file system");

        Driver = null;
        RootPath = null;
        CurrentPath = null;
    }

    public string ResolvePath(string path)
    {
        if (!IsConnected)
            throw new InvalidOperationException("Not connected to any file system");

        if (string.IsNullOrWhiteSpace(path))
            return GetAbsolutePath(CurrentPath ?? "/");

        if (path.StartsWith('/'))
        {
            return GetAbsolutePath(path);
        }

        return GetAbsolutePath(CombinePaths(CurrentPath ?? "/", path));
    }

    public void ChangeDirectory(string path)
    {
        if (!IsConnected || Driver == null)
            throw new InvalidOperationException("Not connected to any file system");

        string absolutePath = ResolvePath(path);

        if (!Driver.Exists(absolutePath))
            throw new DirectoryNotFoundException($"Path '{path}' does not exist");

        if (!Driver.IsDirectory(absolutePath))
            throw new InvalidOperationException($"Path '{path}' is not a directory");

        CurrentPath = GetRelativePath(absolutePath);
    }

    public IFileSystemDriver? Driver { get; private set; }

    private string GetAbsolutePath(string relativePath)
    {
        if (string.IsNullOrEmpty(RootPath))
            throw new InvalidOperationException("Not connected to any file system");

        if (relativePath == "/")
            return RootPath;

        string pathWithoutSlash = relativePath.TrimStart('/');

        return Path.Combine(RootPath, pathWithoutSlash);
    }

    private string? GetRelativePath(string absolutePath)
    {
        if (string.IsNullOrEmpty(RootPath))
            throw new InvalidOperationException("Not connected to any file system");

        if (absolutePath == RootPath)
            return "/";

        if (!absolutePath.StartsWith(RootPath))
            throw new ArgumentException($"Path '{absolutePath}' is outside of root path '{RootPath}'");

        string relative = absolutePath.Substring(RootPath.Length);
        return "/" + relative.TrimStart('\\', '/');
    }
}