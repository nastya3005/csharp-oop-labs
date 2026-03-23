using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public class ParsingResult
{
    public ICommand Command { get; }

    public Dictionary<string, object> Parameters { get; }

    public ParsingResult(ICommand command, Dictionary<string, object> parameters)
    {
        Command = command;
        Parameters = parameters;
    }

    public void ApplyParameters()
    {
        if (Command is IParameterizedCommand parameterizedCommand)
        {
            parameterizedCommand.SetParameters(Parameters);
        }
    }
}