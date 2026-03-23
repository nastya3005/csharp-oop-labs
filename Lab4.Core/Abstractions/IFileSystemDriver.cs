namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Abstractions;

public interface IFileSystemDriver
{
    bool Exists(string path);

    FileInfo GetFileInfo(string path);

    DirectoryInfo GetDirectoryInfo(string path);

    IEnumerable<string> ListDirectory(string path);

    Stream OpenRead(string path);

    void CreateDirectory(string path);

    void Delete(string path);

    void Move(string source, string destination);

    void Copy(string source, string destination);

    bool IsDirectory(string path);

    string GetFullPath(string path);
}