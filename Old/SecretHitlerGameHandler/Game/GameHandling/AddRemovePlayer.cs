namespace SecretHitlerGameHandler.Game.GameHandling;

public static class AddRemovePlayer
{
    public static void Add(this Game game, string username)
    {
        ++game.PlayerCount;
        game.PregamePlayers.Add((byte)(game.PregamePlayers.Count + 1), username);
    }

    public static void Remove(this Game game)
    {
        --game.PlayerCount;
        game.PregamePlayers.Remove((byte)(game.PregamePlayers.Count));
    }
}