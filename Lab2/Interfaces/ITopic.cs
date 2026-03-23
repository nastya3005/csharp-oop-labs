namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface ITopic : IRecipient
{
    string Name { get; }

    void AddRecipient(IRecipient recipient);

    void RemoveRecipient(IRecipient recipient);
}