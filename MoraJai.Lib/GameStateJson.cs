using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoraJai.Lib;

/// <summary>
/// JSON-friendly representation of a game challenge
/// </summary>
public class GameStateJson
{
    [JsonPropertyName("challengeRating")]
    public int ChallengeRating { get; set; }

    [JsonPropertyName("board")]
    public string[][] Board { get; set; } = null!;

    [JsonPropertyName("goal")]
    public string[] Goal { get; set; } = null!;

    /// <summary>
    /// Creates a GameStateJson from a GameState and challenge rating
    /// </summary>
    public static GameStateJson FromGameState(GameState gameState, int challengeRating)
    {
        var board = new string[3][];
        for (int r = 0; r < 3; r++)
        {
            board[r] = new string[3];
            for (int c = 0; c < 3; c++)
            {
                board[r][c] = gameState.Board[r, c].ToString();
            }
        }

        var goal = new string[4];
        for (int i = 0; i < 4; i++)
        {
            goal[i] = gameState.Goal[i].ToString();
        }

        return new GameStateJson
        {
            ChallengeRating = challengeRating,
            Board = board,
            Goal = goal
        };
    }

    /// <summary>
    /// Converts this JSON representation back to a GameState
    /// </summary>
    public GameState ToGameState()
    {
        var board = new TileColor[3, 3];
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                board[r, c] = Enum.Parse<TileColor>(Board[r][c]);
            }
        }

        var goal = new TileColor[4];
        for (int i = 0; i < 4; i++)
        {
            goal[i] = Enum.Parse<TileColor>(Goal[i]);
        }

        return new GameState(board, goal);
    }

    /// <summary>
    /// Serializes to JSON string
    /// </summary>
    public string ToJson(bool indented = true)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = indented
        };
        return JsonSerializer.Serialize(this, options);
    }

    /// <summary>
    /// Deserializes from JSON string
    /// </summary>
    public static GameStateJson? FromJson(string json)
    {
        return JsonSerializer.Deserialize<GameStateJson>(json);
    }
}

/// <summary>
/// Container for multiple challenges
/// </summary>
public class ChallengeCollection
{
    [JsonPropertyName("challenges")]
    public List<ChallengeJson> Challenges { get; set; } = new();

    public string ToJson(bool indented = true)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = indented
        };
        return JsonSerializer.Serialize(this, options);
    }

    public static ChallengeCollection? FromJson(string json)
    {
        return JsonSerializer.Deserialize<ChallengeCollection>(json);
    }
}

/// <summary>
/// Challenge with an ID for collections
/// </summary>
public class ChallengeJson : GameStateJson
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public static ChallengeJson FromGameState(GameState gameState, int id, int challengeRating)
    {
        var baseJson = GameStateJson.FromGameState(gameState, challengeRating);
        return new ChallengeJson
        {
            Id = id,
            ChallengeRating = baseJson.ChallengeRating,
            Board = baseJson.Board,
            Goal = baseJson.Goal
        };
    }
}
