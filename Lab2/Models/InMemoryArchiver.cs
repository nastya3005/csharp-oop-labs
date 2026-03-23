using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class InMemoryArchiver : IArchiver
{
    private readonly List<IMessage> _messages = new List<IMessage>();

    public IReadOnlyCollection<IMessage> Messages => _messages.AsReadOnly();

    public void Save(IMessage msg)
    {
        _messages.Add(msg);
    }

    public void SaveRaw(string msg)
    {
        throw new NotImplementedException();
    }

    public void Recieve(IMessage msg)
    {
        Save(msg);
    }
}