using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class User : IUser
{
    private readonly List<IMessage> _receivedMessages = new();
    private readonly HashSet<IMessage> _readMessages = new();

    public string Name { get; }

    public User(string name)
    {
        Name = name;
    }

    public void MarkAsRead(IMessage msg)
    {
        if (_receivedMessages.Contains(msg))
        {
            if (!_readMessages.Add(msg))
            {
                throw new ArgumentException($"Message {msg} is already read");
            }
        }
    }

    public bool IsMessageRead(IMessage msg)
    {
        return _readMessages.Contains(msg);
    }

    public void Recieve(IMessage msg)
    {
        _receivedMessages.Add(msg);
    }
}