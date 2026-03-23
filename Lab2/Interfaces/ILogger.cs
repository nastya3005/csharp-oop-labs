namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface ILogger
{
    void Log(IRecipient recipient, IMessage message);

    int GetLogCount(IRecipient recipient);
}