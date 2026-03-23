using Itmo.ObjectOrientedProgramming.Lab2.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class LoggingDecorator : IRecipient
{
    private readonly IRecipient _inner;
    private readonly ILogger _logger;

    public LoggingDecorator(IRecipient inner, ILogger logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public void Recieve(IMessage msg)
    {
        _logger.Log(_inner, msg);
        _inner.Recieve(msg);
    }
}