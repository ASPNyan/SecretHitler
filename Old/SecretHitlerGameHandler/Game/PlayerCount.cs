namespace SecretHitlerGameHandler.Game;

public static class PlayerCount
{
    public static void UpdatePlayerCount(this Game game, byte playerCount)
    {
        game.PlayerCount = playerCount;
    }
}