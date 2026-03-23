using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;

public class PositionalParameterHandler : BaseParserHandler
{
    protected override bool CanHandle(ParsingContext context) =>
        !context.HasErrors && context.Metadata != null;

    protected override void HandleInternal(ParsingContext context)
    {
        CommandMetadata? metadata = context.Metadata;
        if (metadata is null) return;

        foreach (string token in context.Tokens)
        {
            ParsePositionalParameter(context, metadata, token);
        }
    }

    private void ParsePositionalParameter(ParsingContext context, CommandMetadata metadata, string token)
    {
        if (context.PositionalParamIndex >= metadata.PositionalParameters.Count)
        {
            context.Errors.Add($"Unexpected positional parameter: {token}");
            return;
        }

        ParameterMetadata paramMeta = metadata.PositionalParameters[context.PositionalParamIndex];
        context.Parameters[paramMeta.Name] = token;
        context.PositionalParamIndex++;
    }
}