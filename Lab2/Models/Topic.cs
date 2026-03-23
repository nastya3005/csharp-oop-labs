using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Topic : ITopic
{
    private readonly List<IRecipient> _recipients = new List<IRecipient>();

    public string Name { get; }

    public Topic(string name)
    {
        Name = name;
    }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void RemoveRecipient(IRecipient recipient)
    {
        _recipients.Remove(recipient);
    }

    public void Recieve(IMessage msg)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.Recieve(msg);
        }
    }
}