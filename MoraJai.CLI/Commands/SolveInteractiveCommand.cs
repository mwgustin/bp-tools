using MoraJai.Lib;

namespace MoraJai.CLI.Commands;

public class SolveInteractiveCommand : ICommand
{
    public void Execute(string[] args)
    {
        // get user input and split into array
        Console.WriteLine("Please enter line 1 of the board:");
        var line1 = Console.ReadLine();
        Console.WriteLine("Please enter line 2 of the board:");
        var line2 = Console.ReadLine();
        Console.WriteLine("Please enter line 3 of the board:");
        var line3 = Console.ReadLine();

        Console.WriteLine("Please enter the goal color(s):");
        var goalColorInput = Console.ReadLine();

        if (line1 is null || line2 is null || line3 is null || goalColorInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        var line1colors = line1.Split(' ');
        var line2colors = line2.Split(' ');
        var line3colors = line3.Split(' ');

        if (line1colors.Length != 3 || line2colors.Length != 3 || line3colors.Length != 3)
        {
            Console.WriteLine("Each line must contain exactly 3 colors");
            return;
        }

        var goalColors = goalColorInput.Split(' ');
        if (goalColors.Length == 1)
        {
            goalColors = new string[] { goalColors[0], goalColors[0], goalColors[0], goalColors[0] };
        }
        else if (goalColors.Length != 4)
        {
            Console.WriteLine("Goal must contain exactly 1 or 4 colors");
            return;
        }

        TileColor[,] board = new TileColor[3, 3];
        TileColor[] goalColor = new TileColor[4];
        try
        {
            // parse colors from input
            for (int i = 0; i < 3; i++)
            {
                board[0, i] = Enum.Parse<TileColor>(line1colors[i], true);
                board[1, i] = Enum.Parse<TileColor>(line2colors[i], true);
                board[2, i] = Enum.Parse<TileColor>(line3colors[i], true);
            }

            for (int i = 0; i < 4; i++)
            {
                goalColor[i] = Enum.Parse<TileColor>(goalColors[i], true);
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid color input. Please use valid color names.");
            Console.WriteLine("Valid color options are:");
            foreach (var color in Enum.GetNames(typeof(TileColor)))
            {
                Console.WriteLine($"- {color.ToLower()}");
            }
            return;
        }

        // create initial game state
        var initState = new GameState(
          board: board,
          Goal: goalColor
        );
        var solver = new Solver();
        var result = solver.SolveStart(initState);

        // print results
        if (result is null) Console.WriteLine("No solution found");
        else
        {
            Console.WriteLine("Solution found:");
            foreach (var item in SolutionNode.GetSolution(result))
            {
                Console.WriteLine(item);
            }
        }
    }
}
