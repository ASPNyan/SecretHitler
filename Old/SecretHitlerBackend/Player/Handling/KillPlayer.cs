namespace SecretHitlerBackend.Player.Handling;

public static class KillPlayer
{
    public static Player Kill(this Player player)
    {
        player.Dead = true;
        return player;
    }
}