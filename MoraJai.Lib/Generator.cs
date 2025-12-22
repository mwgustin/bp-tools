namespace MoraJai.Lib;



public enum ChallengeRating
{
    Easy,
    Medium,
    Hard
}

/// <summary>
/// Generator for creating new initial solvable puzzles
/// </summary>
public class Generator
{

    private ChallengeRating? DepthToChallengeRating(int? depth) => depth switch
    {
        null => null,
        <= 1 => null,
        <= 7 => ChallengeRating.Easy,
        <= 10 => ChallengeRating.Medium,
        _ => ChallengeRating.Hard
    };


    public Dictionary<ChallengeRating, List<GameState>> Generate(int count, int? seed = null)
    {
        Dictionary<ChallengeRating, List<GameState>> successes = new()
        {
            { ChallengeRating.Easy, new() },
            { ChallengeRating.Medium, new() },
            { ChallengeRating.Hard, new() },

        };

        for(int i = 1; i < count; i++)
        {
            (var success, GameState? state, int? depth) = GenerateSolvablePuzzle();
            if(success)
            { 
                var rating = DepthToChallengeRating(depth);
                if (rating is not null && state is not null)
                    successes[rating.Value].Add(state);
            }
        }

        return successes;
    }

    /// <summary>
    /// Generate a puzzle, test its solvability and return the init GameState and deepest node count
    /// </summary>
    /// <returns></returns>
    public (bool, GameState?, int?) GenerateSolvablePuzzle(int? seed = null)
    {
        var randState = GetRandomGameState(seed);

        var solvableState = randState with {};
        var solver = new Solver();

        var node = solver.SolveStart(solvableState);

        if(node is null) return (false, null, null);

        
        return (true, randState, node.Depth);
        
    }

    private GameState GetRandomGameState(int? seed)
    {
        Random rand = seed is null ? new Random() : new Random(seed.GetValueOrDefault());


        // get random set of 9 TileColors
        TileColor[] colorOptions = Enum.GetValues<TileColor>();
        TileColor[] tileColors = new TileColor[9];
        rand.GetItems(colorOptions, tileColors);

        // get solve color from list of tiles
        // only doing single color solves right now
        // can't solve gray tiles, so must filter those out
        TileColor[] nonGrayTiles = tileColors.Where(x => x != TileColor.Gray).Distinct().ToArray();
        TileColor solveColor = nonGrayTiles[rand.Next(nonGrayTiles.Length)];
        TileColor[,] formattedBoard = new TileColor[3,3];
        
        // Map tileColors to 3x3 formattedBoard
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                formattedBoard[i, j] = tileColors[i * 3 + j];
            }
        }
        
        var state= new GameState(formattedBoard, solveColor);
        return state;
    }
}
