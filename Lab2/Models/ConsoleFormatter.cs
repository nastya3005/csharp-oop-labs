using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class ConsoleFormatter : MessageFormatter
{
    public void OutputToConsole(IMessage msg)
    {
        string formatted = Format(msg);
        Console.WriteLine(formatted);
    }
}