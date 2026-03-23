namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface IMessage
{
    string Title { get; }

    string Body { get; }

    int Priority { get; }
}