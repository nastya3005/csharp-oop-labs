using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;

public class CommandNameHandler : BaseParserHandler
{
    private readonly CommandRegistry _registry;

    public CommandNameHandler(CommandRegistry registry)
    {
        _registry = registry;
    }

    protected override bool CanHandle(ParsingContext context) =>
        !context.HasErrors && context.Tokens.Count > 0;

    protected override void HandleInternal(ParsingContext context)
    {
        var tokens = context.Tokens.ToList();

        if (tokens.Count > 1)
        {
            string twoWordCommand = $"{tokens[0]} {tokens[1]}";
            if (_registry.TryGetMetadata(twoWordCommand, out CommandMetadata? metadata))
            {
                context.CommandName = twoWordCommand;
                context.Metadata = metadata;
                tokens.RemoveRange(0, 2);
                context.SetTokens(tokens);
                return;
            }
        }

        if (tokens.Count > 0 && _registry.TryGetMetadata(tokens[0], out CommandMetadata? singleMetadata))
        {
            context.CommandName = tokens[0];
            context.Metadata = singleMetadata;
            tokens.RemoveAt(0);
            context.SetTokens(tokens);
            return;
        }

        context.Errors.Add($"Unknown command: {tokens.FirstOrDefault()}");
    }
}