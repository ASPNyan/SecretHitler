namespace SecretHitlerGameHandler.Game.GameHandling;

public static class AddRemovePlayer
{
    public static void Add(this Game Game)
    {
        ++Game.PlayerCount;
    }

    public static void Remove(this Game Game)
    {
        --Game.PlayerCount;
    }
}