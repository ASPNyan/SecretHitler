namespace SecretHitlerGameHandler.Game;

public static class PlayerCount
{
    public static void UpdatePlayerCount(this Game Game, byte PlayerCount)
    {
        Game.PlayerCount = PlayerCount;
    }
}