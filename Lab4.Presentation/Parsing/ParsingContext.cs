using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public class ParsingContext
{
    public string Input { get; }

    public Collection<string> Tokens { get; private set; }

    public string CommandName { get; set; } = string.Empty;

    public CommandMetadata? Metadata { get; set; }

    public Dictionary<string, object> Parameters { get; } = new(StringComparer.OrdinalIgnoreCase);

    public HashSet<string> ProvidedFlags { get; } = new(StringComparer.OrdinalIgnoreCase);

    public int PositionalParamIndex { get; set; }

    public Collection<string> Errors { get; } = new();

    public bool HasErrors => Errors.Count > 0;

    public ParsingContext(string input)
    {
        Input = input;
        Tokens = new Collection<string>();
    }

    public void ThrowIfHasErrors()
    {
        if (HasErrors)
        {
            throw new ArgumentException(string.Join("; ", Errors));
        }
    }

    public void SetTokens(IEnumerable<string> tokens)
    {
        Tokens = new Collection<string>(tokens.ToList());
    }
}