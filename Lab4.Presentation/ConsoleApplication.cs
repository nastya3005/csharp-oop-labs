using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Base;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Session;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class ConsoleApplication
{
    private readonly CommandParser _parser;
    private readonly FileSystemSession _session;

    public ConsoleApplication()
    {
        _parser = new CommandParser();
        _session = new FileSystemSession();
    }

    public void Run()
    {
        Console.WriteLine("File System Manager v1.0");
        Console.WriteLine("Type 'help' for available commands, 'exit' to quit.");
        Console.WriteLine();

        while (true)
        {
            try
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    ShowHelp();
                    continue;
                }

                ParsingResult parsingResult = _parser.Parse(input);
                ICommand command = parsingResult.Command;

                if (!command.CanExecute(_session))
                {
                    Console.WriteLine($"Cannot execute command '{command.Name}'. Check connection status.");
                    continue;
                }

                CommandResult result = command.Execute(_session, parsingResult.Parameters);

                Console.ForegroundColor = result.IsSuccess ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(result.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    private void ShowHelp()
    {
        Console.WriteLine("=== Quick Reference ===");
        Console.WriteLine("Notation: []=optional, {}=required, -f=flag");
        Console.WriteLine();

        Console.WriteLine("CORE COMMANDS:");
        Console.WriteLine("  connect [Address] [-m Mode]       Connect to FS (disconnected only)");
        Console.WriteLine("  disconnect                        Disconnect from FS (connected only)");
        Console.WriteLine();

        Console.WriteLine("NAVIGATION:");
        Console.WriteLine("  tree goto [Path]                  Change directory");
        Console.WriteLine("  tree list {-d Depth}              List tree with depth");
        Console.WriteLine();

        Console.WriteLine("FILE OPERATIONS:");
        Console.WriteLine("  file show [Path] {-m Mode}        Display file content (mode=console)");
        Console.WriteLine("  file move [Source] [Destination]  Move file");
        Console.WriteLine("  file copy [Source] [Destination]  Copy file");
        Console.WriteLine("  file delete [Path]                Delete file");
        Console.WriteLine("  file rename [Path] [Name]         Rename file (name only, not path)");
        Console.WriteLine();
    }
}