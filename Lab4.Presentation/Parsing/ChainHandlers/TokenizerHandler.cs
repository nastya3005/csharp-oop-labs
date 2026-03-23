using System.Collections.ObjectModel;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;

public class TokenizerHandler : BaseParserHandler
{
    protected override void HandleInternal(ParsingContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Input))
        {
            context.Errors.Add("Input cannot be empty");
            return;
        }

        Collection<string> tokens = Tokenize(context.Input);
        context.SetTokens(tokens);

        if (tokens.Count == 0)
        {
            context.Errors.Add("No command found");
        }
    }

    private static Collection<string> Tokenize(string input)
    {
        var tokens = new List<string>();
        var currentToken = new StringBuilder();
        bool inQuotes = false;

        foreach (char c in input)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                currentToken.Append(c);
            }
            else if (char.IsWhiteSpace(c) && !inQuotes)
            {
                if (currentToken.Length > 0)
                {
                    tokens.Add(ProcessToken(currentToken.ToString()));
                    currentToken.Clear();
                }
            }
            else
            {
                currentToken.Append(c);
            }
        }

        if (currentToken.Length > 0)
        {
            tokens.Add(ProcessToken(currentToken.ToString()));
        }

        return new Collection<string>(tokens);
    }

    private static string ProcessToken(string token)
    {
        if (token.Length >= 2 && token[0] == '"' && token[^1] == '"')
        {
            return token[1..^1];
        }

        return token;
    }
}