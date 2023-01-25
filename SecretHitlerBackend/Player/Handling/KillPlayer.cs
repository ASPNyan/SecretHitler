namespace SecretHitlerBackend.Player.Handling;

public static class KillPlayer
{
    public static Player Kill(this Player Player)
    {
        Player.Dead = true;
        return Player;
    }
}