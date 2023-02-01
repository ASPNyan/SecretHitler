using SecretHitlerGameHandler.Game;

namespace SecretHitlerServerFrontend.Classes;

public class Games
{
    private static readonly Dictionary<uint, Game> GamesList = new();

    public void AddGame(Game Game)
    {
        GamesList.Add(Game.GameId, Game);
    }

    public void RemoveGame(Game Game)
    {
        GamesList.Remove(Game.GameId);
    }
    
    public void RemoveGame(uint GameId)
    {
        GamesList.Remove(GameId);
    }

    public static Game QueryGame(uint GameId)
    {
        return GamesList[GameId];
    }

    public static bool TryQueryGame(uint GameId, out Game Game)
    {
        bool Success = GamesList.TryGetValue(GameId, out Game!);

        return Success;
    }

    public Dictionary<uint, Game> ToDictionary()
    {
        return GamesList;
    }
}