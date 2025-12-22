using MoraJai.Lib;
using MoraJai.CLI.Commands;

// help text
if (args.Length > 0 && (args[0] == "-h" || args[0] == "--help" || args[0] == "help"))
{
    Console.WriteLine("MoraJai CLI - A 3x3 Mora Jai tile color puzzle solver\n");
    Console.WriteLine("Usage:");
    Console.WriteLine("  MoraJai.CLI                      Run in interactive mode");
    Console.WriteLine("  MoraJai.CLI -generate [amount]   Generate new challenge puzzles (default: 50)");
    Console.WriteLine("  MoraJai.CLI -file [path] [id]    Solve a challenge from a file (random if id not specified)");
    Console.WriteLine("  MoraJai.CLI -h | --help        Show this help message\n");
    Console.WriteLine("Interactive Mode:");
    Console.WriteLine("  You will be prompted to enter the colors of each line of the board,");
    Console.WriteLine("  followed by the goal color. Colors should be entered as space-separated values.\n");
    Console.WriteLine("Valid colors:");
    foreach (var color in Enum.GetNames(typeof(TileColor)))
    {
        Console.WriteLine($"  - {color.ToLower()}");
    }
    Console.WriteLine("\nExamples:");
    Console.WriteLine("  MoraJai.CLI -file challenges.json");
    Console.WriteLine("  MoraJai.CLI -file challenges.json 123");
    Console.WriteLine("  MoraJai.CLI -generate");
    Console.WriteLine("  MoraJai.CLI -generate 100");
    return;
}

if(args.Length > 0 && args[0] == "-generate")
{
    new GenerateCommand().Execute(args);
    return;
}

if(args.Length > 0 && args[0] == "-file")
{
    new SolveFileCommand().Execute(args);
    return;
}

// Default: interactive mode
new SolveInteractiveCommand().Execute(args);
