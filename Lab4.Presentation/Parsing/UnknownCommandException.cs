namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public class UnknownCommandException : ParsingException
{
    public string CommandName { get; }

    public UnknownCommandException(string commandName)
        : base($"Unknown command: {commandName}")
    {
        CommandName = commandName;
    }
}