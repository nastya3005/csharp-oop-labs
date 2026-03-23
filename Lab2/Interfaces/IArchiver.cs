namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface IArchiver : IRecipient
{
    void Save(IMessage msg);

    void SaveRaw(string msg);
}