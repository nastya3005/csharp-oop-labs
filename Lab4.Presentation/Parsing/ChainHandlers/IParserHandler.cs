namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ChainHandlers;

public interface IParserHandler
{
    IParserHandler SetNext(IParserHandler handler);

    void Handle(ParsingContext context);
}

public abstract class BaseParserHandler : IParserHandler
{
    public void Handle(ParsingContext context)
    {
        if (CanHandle(context))
        {
            HandleInternal(context);
        }

        _nextHandler?.Handle(context);
    }

    private IParserHandler? _nextHandler;

    public IParserHandler SetNext(IParserHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    protected virtual bool CanHandle(ParsingContext context) => true;

    protected abstract void HandleInternal(ParsingContext context);
}