namespace Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

public interface IFormatter
{
    string Format(IMessage msg);

    string FormatTitle(IMessage msg);

    string FormatBody(IMessage msg);
}