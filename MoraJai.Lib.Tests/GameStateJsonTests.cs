namespace MoraJai.Lib.Tests;

public class GameStateJsonTests
{
    [Test]
    public async Task FromGameState_Should_CreateValidJson()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Blue, TileColor.Green },
            { TileColor.Yellow, TileColor.Orange, TileColor.Pink },
            { TileColor.Gray, TileColor.Black, TileColor.Violet }
        };
        var goal = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Yellow };
        var gameState = new GameState(board, goal);

        // Act
        var gameStateJson = GameStateJson.FromGameState(gameState, challengeRating: 5);

        // Assert
        await VerifyJson(gameStateJson.ToJson());
    }

    [Test]
    public async Task ToGameState_Should_RoundTripCorrectly()
    {
        // Arrange
        var originalBoard = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Blue, TileColor.Green },
            { TileColor.Yellow, TileColor.Orange, TileColor.Pink },
            { TileColor.Gray, TileColor.Black, TileColor.Violet }
        };
        var originalGoal = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Yellow };
        var originalGameState = new GameState(originalBoard, originalGoal);
        var gameStateJson = GameStateJson.FromGameState(originalGameState, challengeRating: 3);

        // Act
        var roundTrippedGameState = gameStateJson.ToGameState();

        // Assert
        await Assert.That(roundTrippedGameState.Board).IsEquivalentTo(originalGameState.Board);
        await Assert.That(roundTrippedGameState.Goal).IsEquivalentTo(originalGameState.Goal);
    }

    [Test]
    public async Task ToJson_Should_ProduceValidJsonString()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Red, TileColor.Red },
            { TileColor.Blue, TileColor.Blue, TileColor.Blue },
            { TileColor.Green, TileColor.Green, TileColor.Green }
        };
        var goal = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Red };
        var gameState = new GameState(board, goal);
        var gameStateJson = GameStateJson.FromGameState(gameState, challengeRating: 1);

        // Act
        var json = gameStateJson.ToJson(indented: true);

        // Assert
        await VerifyJson(json);
    }

    [Test]
    public async Task ToJson_Compact_Should_ProduceValidJsonString()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Red, TileColor.Red },
            { TileColor.Blue, TileColor.Blue, TileColor.Blue },
            { TileColor.Green, TileColor.Green, TileColor.Green }
        };
        var goal = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Red };
        var gameState = new GameState(board, goal);
        var gameStateJson = GameStateJson.FromGameState(gameState, challengeRating: 1);

        // Act
        var json = gameStateJson.ToJson(indented: false);

        // Assert
        await Verify(json);
    }

    [Test]
    public async Task FromJson_Should_DeserializeCorrectly()
    {
        // Arrange
        var json = """
        {
          "challengeRating": 2,
          "board": [
            ["Red", "Blue", "Green"],
            ["Yellow", "Orange", "Pink"],
            ["Gray", "Black", "Violet"]
          ],
          "goal": ["Red", "Blue", "Green", "Yellow"]
        }
        """;

        // Act
        var gameStateJsonObj = GameStateJson.FromJson(json);

        // Assert
        gameStateJsonObj = await Assert.That(gameStateJsonObj).IsNotNull();
        await Verify(gameStateJsonObj);
    }

    [Test]
    public async Task FromJson_ToJson_RoundTrip_Should_PreserveData()
    {
        // Arrange
        var originalJson = """
        {
          "challengeRating": 1,
          "board": [
            ["Red", "Blue", "Green"],
            ["Yellow", "Orange", "Pink"],
            ["Gray", "Black", "Violet"]
          ],
          "goal": ["Violet", "Pink", "Orange", "Yellow"]
        }
        """;
        var gameStateJson = GameStateJson.FromJson(originalJson);

        // Act
        var roundTrippedJson = gameStateJson?.ToJson();

        // Assert
        await VerifyJson(roundTrippedJson);
    }

    [Test]
    public async Task ChallengeJson_FromGameState_Should_IncludeId()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Blue, TileColor.Green },
            { TileColor.Yellow, TileColor.Orange, TileColor.Pink },
            { TileColor.Gray, TileColor.Black, TileColor.Violet }
        };
        var goal = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Yellow };
        var gameState = new GameState(board, goal);

        // Act
        var challengeJson = ChallengeJson.FromGameState(gameState, id: 42, challengeRating: 2);

        // Assert
        await VerifyJson(challengeJson.ToJson());
    }

    [Test]
    public async Task ChallengeCollection_Should_SerializeMultipleChallenges()
    {
        // Arrange
        var collection = new ChallengeCollection();
        
        var board1 = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Red, TileColor.Red },
            { TileColor.Blue, TileColor.Blue, TileColor.Blue },
            { TileColor.Green, TileColor.Green, TileColor.Green }
        };
        var goal1 = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Red };
        var gameState1 = new GameState(board1, goal1);
        
        var board2 = new TileColor[3, 3]
        {
            { TileColor.Yellow, TileColor.Orange, TileColor.Pink },
            { TileColor.Gray, TileColor.Black, TileColor.Violet },
            { TileColor.Red, TileColor.Blue, TileColor.Green }
        };
        var goal2 = new TileColor[] { TileColor.Yellow, TileColor.Orange, TileColor.Pink, TileColor.Gray };
        var gameState2 = new GameState(board2, goal2);

        collection.Challenges.Add(ChallengeJson.FromGameState(gameState1, id: 1, challengeRating: 2));
        collection.Challenges.Add(ChallengeJson.FromGameState(gameState2, id: 2, challengeRating: 3));

        // Act
        var json = collection.ToJson(indented: true);

        // Assert
        await VerifyJson(json);
    }

    [Test]
    public async Task ChallengeCollection_FromJson_Should_DeserializeCorrectly()
    {
        // Arrange
        var json = """
        {
          "challenges": [
            {
              "id": 1,
              "challengeRating": 2,
              "board": [
                ["Red", "Red", "Red"],
                ["Blue", "Blue", "Blue"],
                ["Green", "Green", "Green"]
              ],
              "goal": ["Red", "Blue", "Green", "Red"]
            },
            {
              "id": 2,
              "challengeRating": 3,
              "board": [
                ["Yellow", "Orange", "Pink"],
                ["Gray", "Black", "Violet"],
                ["Red", "Blue", "Green"]
              ],
              "goal": ["Yellow", "Orange", "Pink", "Gray"]
            }
          ]
        }
        """;

        // Act
        var collection = ChallengeCollection.FromJson(json);

        // Assert
        collection = await Assert.That(collection).IsNotNull();
        await Verify(collection);
    }

    [Test]
    public async Task ChallengeCollection_RoundTrip_Should_PreserveAllData()
    {
        // Arrange
        var originalCollection = new ChallengeCollection();
        
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Blue, TileColor.Green },
            { TileColor.Yellow, TileColor.Orange, TileColor.Pink },
            { TileColor.Gray, TileColor.Black, TileColor.Violet }
        };
        var goal = new TileColor[] { TileColor.Red, TileColor.Blue, TileColor.Green, TileColor.Yellow };
        var gameState = new GameState(board, goal);

        originalCollection.Challenges.Add(ChallengeJson.FromGameState(gameState, id: 99, challengeRating: 1));

        // Act - Serialize then Deserialize
        var json = originalCollection.ToJson();
        var roundTrippedCollection = ChallengeCollection.FromJson(json);

        // Assert
        roundTrippedCollection = await Assert.That(roundTrippedCollection).IsNotNull();
        await Verify(roundTrippedCollection);
    }
}
