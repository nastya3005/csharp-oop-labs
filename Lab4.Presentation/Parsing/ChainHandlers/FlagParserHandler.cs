namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;

public class FlagParserHandler : BaseParserHandler
{
    protected override bool CanHandle(ParsingContext context) =>
        !context.HasErrors && context.Metadata != null;

    protected override void HandleInternal(ParsingContext context)
    {
        if (context.Metadata is null) return;

        var tokens = context.Tokens.ToList();
        var processedTokens = new List<string>();

        for (int i = 0; i < tokens.Count; i++)
        {
            string token = tokens[i];

            if (token.StartsWith('-'))
            {
                if (!ParseFlag(context, token, tokens, i, out int newIndex))
                {
                    i = newIndex;
                    continue;
                }

                if (newIndex > i)
                {
                    i = newIndex;
                }
            }
            else
            {
                processedTokens.Add(token);
            }
        }

        context.SetTokens(processedTokens);

        ApplyDefaultValues(context);
    }

    private void ApplyDefaultValues(ParsingContext context)
    {
        if (context.Metadata is null) return;

        foreach (KeyValuePair<string, Metadata.FlagMetadata> flagPair in context.Metadata.Flags)
        {
            Metadata.FlagMetadata flagMeta = flagPair.Value;

            if (!context.ProvidedFlags.Contains(flagPair.Key) && flagMeta.DefaultValue != null)
            {
                context.Parameters[flagPair.Key] = flagMeta.DefaultValue;
                context.ProvidedFlags.Add(flagPair.Key);
            }
        }
    }

    private bool ParseFlag(ParsingContext context, string token, List<string> tokens, int currentIndex, out int newIndex)
    {
        newIndex = currentIndex;
        string flagName = token.TrimStart('-');

        if (context.Metadata != null && context.Metadata.Flags.TryGetValue(flagName, out Metadata.FlagMetadata? flagMeta))
        {
            context.ProvidedFlags.Add(flagName);

            if (flagMeta.HasValue)
            {
                if (currentIndex + 1 >= tokens.Count || tokens[currentIndex + 1].StartsWith('-'))
                {
                    context.Errors.Add($"Flag -{flagName} requires a value");
                    return false;
                }

                context.Parameters[flagName] = tokens[currentIndex + 1];
                newIndex = currentIndex + 1;
            }
            else
            {
                context.Parameters[flagName] = true;
            }

            return true;
        }

        context.Errors.Add($"Unknown flag: -{flagName}");
        return false;
    }
}