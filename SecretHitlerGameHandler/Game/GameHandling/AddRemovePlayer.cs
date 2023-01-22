namespace SecretHitlerGameHandler.Game.GameHandling;

public static class AddRemovePlayer
{
    public static void Add(this Game Game, string Username)
    {
        ++Game.PlayerCount;
        Game.PregamePlayers.Add((byte)(Game.PregamePlayers.Count + 1), Username);
    }

    public static void Remove(this Game Game)
    {
        --Game.PlayerCount;
        Game.PregamePlayers.Remove((byte)(Game.PregamePlayers.Count));
    }
}