using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;

public interface ICommand
{
    string Name { get; }

    string Description { get; }

    CommandResult Execute(FileSystemSession session, IReadOnlyDictionary<string, object> parameters);

    bool CanExecute(FileSystemSession session);
}