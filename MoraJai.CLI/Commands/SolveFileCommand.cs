using MoraJai.Lib;

namespace MoraJai.CLI.Commands;

public class SolveFileCommand : ICommand
{
    public void Execute(string[] args)
    {
        var filePath = args.Length > 1 ? args[1] : "challenges.json";
        if(!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }
        string json = File.ReadAllText(filePath);
        var collection = ChallengeCollection.FromJson(json);
        if(collection is null || collection.Challenges.Count == 0)
        {
            Console.WriteLine("No challenges found in the file.");
            return;
        }

        Console.WriteLine($"Loaded {collection.Challenges.Count} challenges from {filePath}");

        // Select challenge by ID if provided, otherwise random
        ChallengeJson? challenge;
        if (args.Length > 2 && int.TryParse(args[2], out int challengeId))
        {
            challenge = collection.Challenges.FirstOrDefault(c => c.Id == challengeId);
            if (challenge is null)
            {
                Console.WriteLine($"Challenge with ID {challengeId} not found.");
                return;
            }
        }
        else
        {
            Random rand = new Random();
            challenge = collection.Challenges[rand.Next(collection.Challenges.Count)];
        }
        Console.WriteLine($"Solving Challenge ID: {challenge.Id}, Rating: {challenge.ChallengeRating}");
        var loadedInitState = challenge.ToGameState();
        var solver = new Solver();
        var result = solver.SolveStart(loadedInitState);
        
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
