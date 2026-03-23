using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Logger : ILogger
{
    private readonly Dictionary<IRecipient, int> _counts = new();

    public void Log(IRecipient recipient, IMessage message)
    {
        _counts[recipient] = _counts.GetValueOrDefault(recipient) + 1;
    }

    public int GetLogCount(IRecipient recipient)
    {
        return _counts.GetValueOrDefault(recipient);
    }
}