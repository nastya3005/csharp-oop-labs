namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface IUser : IRecipient
{
    string Name { get; }

    void MarkAsRead(IMessage msg);

    bool IsMessageRead(IMessage msg);
}