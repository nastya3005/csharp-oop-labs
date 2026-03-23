namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface IAlert : IRecipient
{
    void TriggerAlert();

    void AddKeyword(string keyword);

    void RemoveKeyword(string keyword);
}