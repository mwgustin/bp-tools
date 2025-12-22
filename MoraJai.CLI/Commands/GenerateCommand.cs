using MoraJai.Lib;

namespace MoraJai.CLI.Commands;

public class GenerateCommand : ICommand
{
    public void Execute(string[] args)
    {
        var gen = new Generator();
        var amount = 50;
        
        // Parse custom amount from args if provided
        if (args.Length > 1 && int.TryParse(args[1], out int customAmount) && customAmount > 0)
        {
            amount = customAmount;
        }
        
        var genResults = gen.Generate(amount);

        int total = genResults.Sum(x => x.Value.Count);
        int easy = genResults.Where(x => x.Key == ChallengeRating.Easy).Select(x => x.Value.Count).First();
        int medium = genResults.Where(x => x.Key == ChallengeRating.Medium).Select(x => x.Value.Count).First();
        int hard = genResults.Where(x => x.Key == ChallengeRating.Hard).Select(x => x.Value.Count).First();
        Console.WriteLine($"{total} successes of {amount} generated");
        Console.WriteLine($"Easy: {easy}");
        Console.WriteLine($"Medium: {medium}");
        Console.WriteLine($"Hard: {hard}");

        // Load existing collection or create new one
        string filePath = "challenges.json";
        ChallengeCollection collection;
        int nextId = 1;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            collection = ChallengeCollection.FromJson(existingJson) ?? new ChallengeCollection();
            nextId = collection.Challenges.Count > 0 ? collection.Challenges.Max(c => c.Id) + 1 : 1;
            Console.WriteLine($"Loaded {collection.Challenges.Count} existing challenges from {filePath}");
        }
        else
        {
            collection = new ChallengeCollection();
            Console.WriteLine($"Creating new challenge file: {filePath}");
        }

        // Convert rating enum to numeric value
        int GetRatingValue(ChallengeRating rating) => (int)rating;

        // Add new challenges to collection
        int addedCount = 0;
        foreach(var rating in genResults.Keys)
        {
            var states = genResults[rating];
            int count = 1;
            foreach(var state in states)
            {
                Console.WriteLine($"--- {rating} Puzzle {count} ---");
                Console.WriteLine(state);
                
                var challenge = ChallengeJson.FromGameState(state, nextId, GetRatingValue(rating));
                collection.Challenges.Add(challenge);
                nextId++;
                addedCount++;
                count++;
            }
        }

        // Save to file
        string json = collection.ToJson();
        File.WriteAllText(filePath, json);
        Console.WriteLine($"\nSaved {addedCount} new challenges to {filePath} (Total: {collection.Challenges.Count})");
    }
}
