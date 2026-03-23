using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public abstract class MessageFormatter : IFormatter
{
    public string Format(IMessage msg)
    {
        return FormatTitle(msg) + '\n' + FormatBody(msg);
    }

    public virtual string FormatTitle(IMessage msg)
    {
        return $"# {msg.Title} (Priority: {msg.Priority})";
    }

    public virtual string FormatBody(IMessage msg)
    {
        return $"{msg.Body}";
    }
}