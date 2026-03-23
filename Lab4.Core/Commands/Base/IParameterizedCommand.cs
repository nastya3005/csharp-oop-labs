namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;

public interface IParameterizedCommand : ICommand
{
    void SetParameters(Dictionary<string, object> parameters);
}