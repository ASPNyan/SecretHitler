using SecretHitlerGameHandler.Game;

namespace SecretHitlerServerFrontend.Classes;

public class Games
{
    private static readonly Dictionary<ulong, Game> GamesList = new();

    public void AddGame(Game Game)
    {
        GamesList.Add(Game.GameId, Game);
    }

    public void RemoveGame(Game Game)
    {
        GamesList.Remove(Game.GameId);
    }
    
    public void RemoveGame(ulong GameId)
    {
        GamesList.Remove(GameId);
    }

    public Game QueryGame(ulong GameId)
    {
        return GamesList[GameId];
    }

    public Game QueryGame(Game Game)
    {
        return GamesList[Game.GameId];
    }

    public Dictionary<ulong, Game> ToDictionary()
    {
        return GamesList;
    }
}