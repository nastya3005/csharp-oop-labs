namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;

public class ValidationHandler : BaseParserHandler
{
    protected override bool CanHandle(ParsingContext context) =>
        !context.HasErrors && context.Metadata != null;

    protected override void HandleInternal(ParsingContext context)
    {
        if (context.Metadata is null) return;

        ValidatePositionalParameters(context);
        ValidateFlags(context);
        context.ThrowIfHasErrors();
    }

    private void ValidatePositionalParameters(ParsingContext context)
    {
        Metadata.CommandMetadata? metadata = context.Metadata;
        if (metadata is null) return;

        for (int i = 0; i < metadata.PositionalParameters.Count; i++)
        {
            Metadata.ParameterMetadata paramMeta = metadata.PositionalParameters[i];
            string paramKey = paramMeta.Name;

            if (paramMeta.IsRequired && !context.Parameters.ContainsKey(paramKey))
            {
                context.Errors.Add($"Required positional parameter '{paramKey}' is missing");
            }
        }
    }

    private void ValidateFlags(ParsingContext context)
    {
        Metadata.CommandMetadata? metadata = context.Metadata;
        if (metadata is null) return;

        foreach (KeyValuePair<string, Metadata.FlagMetadata> flagPair in metadata.Flags)
        {
            Metadata.FlagMetadata flagMeta = flagPair.Value;

            if (flagMeta.IsRequired && !context.ProvidedFlags.Contains(flagMeta.Name))
            {
                context.Errors.Add($"Required flag -{flagMeta.Name} is missing");
            }
        }
    }
}