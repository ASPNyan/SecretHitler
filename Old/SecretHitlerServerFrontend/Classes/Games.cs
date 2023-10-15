using SecretHitlerGameHandler.Game;

namespace SecretHitlerServerFrontend.Classes;

public class Games
{
    private static readonly Dictionary<uint, Game> GamesList = new();

    public static void AddGame(Game game)
    {
        GamesList.Add(game.GameId, game);
    }

    public static void RemoveGame(Game game)
    {
        GamesList.Remove(game.GameId);
    }
    
    public static void RemoveGame(uint gameId)
    {
        GamesList.Remove(gameId);
    }

    public static Game QueryGame(uint gameId)
        => GamesList[gameId];

    public static bool TryQueryGame(uint gameId, out Game game) 
        => GamesList.TryGetValue(gameId, out game!);

    public static Dictionary<uint, Game> ToDictionary()
        => GamesList;
}