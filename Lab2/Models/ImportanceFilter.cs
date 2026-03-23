using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class ImportanceFilter : IRecipient
{
    private readonly IRecipient _recipient;

    public int MinPriority { get; }

    public int MaxPriority { get; }

    public ImportanceFilter(IRecipient recipient, int minPriority, int maxPriority)
    {
        _recipient = recipient;
        MinPriority = minPriority;
        MaxPriority = maxPriority;
    }

    public void Recieve(IMessage msg)
    {
        if (msg.Priority >= MinPriority && msg.Priority <= MaxPriority)
        {
            _recipient.Recieve(msg);
        }
    }
}