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

    public Game QueryGame(uint GameId)
    {
        return GamesList[GameId];
    }

    public Game QueryGame(Game Game)
    {
        return GamesList[Game.GameId];
    }

    public Dictionary<uint, Game> ToDictionary()
    {
        return GamesList;
    }
}