using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class FormattingArchiver : IArchiver
{
    private readonly IArchiver _archiver;
    private readonly IFormatter _formatter;

    public FormattingArchiver(IArchiver archiver, IFormatter formatter)
    {
        _archiver = archiver;
        _formatter = formatter;
    }

    public void Save(IMessage msg)
    {
        string formatted = _formatter.Format(msg);
        _archiver.SaveRaw(formatted);
    }

    public void SaveRaw(string msg)
    {
        _archiver.SaveRaw(msg);
    }

    public void Recieve(IMessage msg)
    {
        Save(msg);
    }
}