using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

public class CommandMetadata
{
    public Type CommandType { get; }

    public Collection<ParameterMetadata> PositionalParameters { get; }

    public Dictionary<string, FlagMetadata> Flags { get; }

    public CommandMetadata(Type commandType)
    {
        CommandType = commandType;
        PositionalParameters = new Collection<ParameterMetadata>();
        Flags = new Dictionary<string, FlagMetadata>(StringComparer.OrdinalIgnoreCase);
    }
}