using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class FileFormatter : MessageFormatter
{
    private readonly string _filePath;

    public FileFormatter(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveToFile(IMessage msg)
    {
        string formatted = Format(msg);
        File.AppendAllText(_filePath, formatted + Environment.NewLine + Environment.NewLine);
    }
}