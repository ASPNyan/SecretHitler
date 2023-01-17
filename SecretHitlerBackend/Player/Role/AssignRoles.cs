namespace SecretHitlerBackend.Player.Role;

public static class AssignRoles
{
    public static void Assign(this Role Player, PlayerRole Role)
    {
        Player.PlayerRole = Role;
    }
}