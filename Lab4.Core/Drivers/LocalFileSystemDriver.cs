using Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Drivers;

public class LocalFileSystemDriver : IFileSystemDriver
{
    public bool Exists(string path)
    {
        return File.Exists(path) || Directory.Exists(path);
    }

    public FileInfo GetFileInfo(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found: {path}");

        return new FileInfo(path);
    }

    public DirectoryInfo GetDirectoryInfo(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory not found: {path}");

        return new DirectoryInfo(path);
    }

    public IEnumerable<string> ListDirectory(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory not found: {path}");

        var entries = new List<string>();

        foreach (string dir in Directory.GetDirectories(path))
        {
            entries.Add(Path.GetFileName(dir));
        }

        foreach (string file in Directory.GetFiles(path))
        {
            if (file == ".." || file == ".")
            {
                continue;
            }

            entries.Add(Path.GetFileName(file));
        }

        return entries;
    }

    public Stream OpenRead(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found: {path}");

        return File.OpenRead(path);
    }

    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public void Delete(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else if (Directory.Exists(path))
        {
            if (Directory.GetFileSystemEntries(path).Length > 0)
                throw new IOException($"Directory '{path}' is not empty");

            Directory.Delete(path);
        }
        else
        {
            throw new FileNotFoundException($"Path not found: {path}");
        }
    }

    public void Move(string source, string destination)
    {
        if (File.Exists(source))
        {
            if (Directory.Exists(destination))
            {
                string fileName = Path.GetFileName(source);
                destination = Path.Combine(destination, fileName);
            }

            File.Move(source, destination);
        }
        else if (Directory.Exists(source))
        {
            Directory.Move(source, destination);
        }
        else
        {
            throw new FileNotFoundException($"Source not found: {source}");
        }
    }

    public void Copy(string source, string destination)
    {
        if (File.Exists(source))
        {
            if (Directory.Exists(destination))
            {
                string fileName = Path.GetFileName(source);
                destination = Path.Combine(destination, fileName);
            }

            File.Copy(source, destination);
        }
        else
        {
            throw new FileNotFoundException($"Source file not found: {source}");
        }
    }

    public bool IsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    public string GetFullPath(string path)
    {
        return Path.GetFullPath(path);
    }
}