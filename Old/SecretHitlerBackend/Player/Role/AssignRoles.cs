namespace SecretHitlerBackend.Player.Role;

public static class AssignRoles
{
    public static void Assign(this Role player, PlayerRole role)
    {
        player.PlayerRole = role;
    }
}