using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Message : IMessage
{
    public string Title { get; }

    public string Body { get; }

    public int Priority { get; }

    public Message(string title, string body, int priority)
    {
        Title = title;
        Body = body;
        Priority = priority;
    }
}