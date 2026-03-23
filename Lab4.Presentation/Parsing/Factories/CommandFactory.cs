using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Factories;

public class CommandFactory : ICommandFactory
{
    public ICommand Create(CommandMetadata metadata, Dictionary<string, object> parameters)
    {
        if (Activator.CreateInstance(metadata.CommandType) is not ICommand command)
        {
            throw new InvalidOperationException(
                $"Failed to create command instance for {metadata.CommandType.Name}");
        }

        if (command is IParameterizedCommand parameterizedCommand)
        {
            parameterizedCommand.SetParameters(parameters);
        }

        return command;
    }
}