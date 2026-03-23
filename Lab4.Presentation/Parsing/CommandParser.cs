using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Factories;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public class CommandParser
{
    private readonly CommandRegistry _registry;
    private readonly CommandFactory _factory;
    private readonly TokenizerHandler _parsingPipeline;

    public CommandParser()
    {
        _registry = new CommandRegistry();
        _factory = new CommandFactory();
        _parsingPipeline = BuildParsingPipeline();
    }

    public ParsingResult Parse(string input)
    {
        var context = new ParsingContext(input);

        _parsingPipeline.Handle(context);

        context.ThrowIfHasErrors();

        if (context.Metadata is null)
        {
            throw new ArgumentException("Command metadata not found");
        }

        Core.Commands.Base.ICommand command = _factory.Create(context.Metadata, context.Parameters);

        return new ParsingResult(command, context.Parameters);
    }

    private TokenizerHandler BuildParsingPipeline()
    {
        var tokenizer = new TokenizerHandler();
        var commandName = new CommandNameHandler(_registry);
        var flagParser = new FlagParserHandler();
        var positionalParser = new PositionalParameterHandler();
        var validator = new ValidationHandler();

        tokenizer
            .SetNext(commandName)
            .SetNext(flagParser)
            .SetNext(positionalParser)
            .SetNext(validator);

        return tokenizer;
    }
}