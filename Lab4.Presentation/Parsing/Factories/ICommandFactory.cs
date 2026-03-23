using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Factories;

public interface ICommandFactory
{
    ICommand Create(CommandMetadata metadata, Dictionary<string, object> parameters);
}