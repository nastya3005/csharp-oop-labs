using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public abstract class AAlert : IAlert
{
    private readonly HashSet<string> _keywords = new HashSet<string>();

    public void Recieve(IMessage msg)
    {
        foreach (string keyword in _keywords)
        {
            if (msg.Body.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                TriggerAlert();
            }
        }
    }

    public abstract void TriggerAlert();

    public void AddKeyword(string keyword)
    {
        _keywords.Add(keyword);
    }

    public void RemoveKeyword(string keyword)
    {
        _keywords.Remove(keyword);
    }
}